using AutoMapper;
using School.Dtos.StudentDtos;
using School.DTOs;
using School.Enums;
using School.Models;
using School.Repositories.Interfaces;
using School.Services.Interfaces;

namespace School.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;
        private readonly IGradeRepository _gradeRepository;
        private readonly IClassSubjectRepository _classSubjectRepository;

        public StudentService(
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            IAuditService auditService,
            IMapper mapper,
            IGradeRepository gradeRepository,
            IClassSubjectRepository classSubjectRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _auditService = auditService;
            _mapper = mapper;
            _gradeRepository = gradeRepository;
            _classSubjectRepository = classSubjectRepository;
        }

        public async Task<List<StudentResponseDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<List<StudentResponseDto>>(students);
        }

        public async Task<StudentResponseDto?> GetByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student == null ? null : _mapper.Map<StudentResponseDto>(student);
        }

        public async Task<List<StudentResponseDto>> GetInactiveAsync()
        {
            var students = await _studentRepository.GetInactiveAsync();
            return _mapper.Map<List<StudentResponseDto>>(students);
        }

        public async Task<StudentResponseDto> ReactivateAsync(Guid id, Guid performedBySecretaryId)
        {
            var student = await _studentRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Aluno não encontrado.");

            if (student.IsActive)
                throw new InvalidOperationException("Este aluno já está ativo.");

            student.IsActive = true;
            await _studentRepository.UpdateAsync(student);

            await _auditService.LogAsync(
                entityName: "Student",
                entityId: student.Id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Aluno '{student.Name}' foi reativado."
            );

            return _mapper.Map<StudentResponseDto>(student);
        }

        public async Task<StudentResponseDto> CreateAsync(CreateStudentDto dto, Guid performedBySecretaryId)
        {
            var cpfEmUso = await _studentRepository.ExistsByCpfAsync(dto.Cpf);
            if (cpfEmUso)
                throw new InvalidOperationException("Já existe um(a) aluno(a) cadastrado com este CPF.");

            var registrationEmUso = await _studentRepository.ExistsByRegistrationAsync(dto.Registration);
            if (registrationEmUso)
                throw new InvalidOperationException("Já existe um(a) aluno(a) cadastrado com esta matrícula.");

            var turma = await _classRepository.GetByIdAsync(dto.ClassId)
                ?? throw new KeyNotFoundException("Turma informada não encontrada.");

            var student = new Student
            {
                Name = dto.Name,
                Cpf = dto.Cpf,
                Registration = dto.Registration,
                DateOfBirth = dto.DateOfBirth,
                ClassId = dto.ClassId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _studentRepository.AddAsync(student);

            await _auditService.LogAsync(
                entityName: "Student",
                entityId: student.Id,
                action: Actions.Created,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Aluno(a) '{student.Name}' (Matrícula: {student.Registration}) foi cadastrado(a) na turma '{turma.Name}'."
            );

            student.Class = turma; // garante o ClassName no retorno, sem precisar buscar de novo
            return _mapper.Map<StudentResponseDto>(student);
        }

        // No StudentService.cs, SUBSTITUIR o método UpdateAsync inteiro por esta versão:

        public async Task<StudentResponseDto> UpdateAsync(Guid id, UpdateStudentDto dto, Guid performedBySecretaryId)
        {
            var student = await _studentRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Aluno(a) não encontrado.");

            var turma = await _classRepository.GetByIdAsync(dto.ClassId)
                ?? throw new KeyNotFoundException("Turma informada não encontrada.");

            var oldName = student.Name;
            var oldClassId = student.ClassId;

            // Busca a turma ANTIGA antes de sobrescrever, só precisamos dela pra comparar o AcademicYear
            var oldClass = await _classRepository.GetByIdAsync(oldClassId);

            student.Name = dto.Name;
            student.DateOfBirth = dto.DateOfBirth;
            student.ClassId = dto.ClassId;

            await _studentRepository.UpdateAsync(student);

            var mudouDeTurma = oldClassId != dto.ClassId;
            var mesmoAnoLetivo = oldClass != null && oldClass.AcademicYear == turma.AcademicYear;

            // Só migra as notas se for uma TRANSFERÊNCIA dentro do mesmo ano letivo.
            // Se o AcademicYear mudou (ex: passou de ano), as notas antigas ficam
            // como histórico, vinculadas ao vínculo (ClassSubject) do ano anterior.
            if (mudouDeTurma && mesmoAnoLetivo)
            {
                var studentGrades = await _gradeRepository.GetByStudentIdAsync(student.Id);

                foreach (var grade in studentGrades)
                {
                    var subjectId = grade.ClassSubject.SubjectId;

                    if (grade.ClassSubject.ClassId == dto.ClassId)
                        continue;

                    var newClassSubject = await _classSubjectRepository.GetByClassAndSubjectAsync(dto.ClassId, subjectId);

                    if (newClassSubject != null)
                    {
                        grade.ClassSubjectId = newClassSubject.Id;
                        await _gradeRepository.UpdateAsync(grade);
                    }
                }
            }

            await _auditService.LogAsync(
                entityName: "Student",
                entityId: id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Aluno(a) atualizado. Nome: '{oldName}' -> '{student.Name}'."
                    + (mudouDeTurma ? $" Turma alterada para '{turma.Name}'." : "")
                    + (mudouDeTurma && !mesmoAnoLetivo ? " (mudança de ano letivo — notas antigas preservadas como histórico)" : "")
            );

            student.Class = turma;
            return _mapper.Map<StudentResponseDto>(student);
        }

        public async Task DeleteAsync(Guid id, Guid performedBySecretaryId)
        {
            var student = await _studentRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Aluno não encontrado.");

            if (!student.IsActive)
                throw new InvalidOperationException("Este aluno já está desativado.");

            student.IsActive = false;
            await _studentRepository.UpdateAsync(student);

            await _auditService.LogAsync(
                entityName: "Student",
                entityId: student.Id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Aluno '{student.Name}' foi desativado (removido da lista ativa)."
            );
        }
    }
}