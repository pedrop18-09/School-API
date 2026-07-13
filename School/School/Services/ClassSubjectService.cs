using AutoMapper;
using School.Dtos.ClassSubjectDtos;
using School.DTOs;
using School.Enums;
using School.Models;
using School.Repositories.Interfaces;
using School.Services.Interfaces;

namespace School.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        private readonly IClassSubjectRepository _classSubjectRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;

        public ClassSubjectService(
            IClassSubjectRepository classSubjectRepository,
            IClassRepository classRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository,
            IAuditService auditService,
            IMapper mapper)
        {
            _classSubjectRepository = classSubjectRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _auditService = auditService;
            _mapper = mapper;
        }

        public async Task<List<ClassSubjectResponseDto>> GetAllAsync()
        {
            var classSubjects = await _classSubjectRepository.GetAllAsync();
            return _mapper.Map<List<ClassSubjectResponseDto>>(classSubjects);
        }

        public async Task<ClassSubjectResponseDto?> GetByIdAsync(Guid id)
        {
            var classSubject = await _classSubjectRepository.GetByIdAsync(id);
            return classSubject == null ? null : _mapper.Map<ClassSubjectResponseDto>(classSubject);
        }

        public async Task<ClassSubjectResponseDto> CreateAsync(CreateClassSubjectDto dto, Guid performedBySecretaryId)
        {
            var turma = await _classRepository.GetByIdAsync(dto.ClassId)
                ?? throw new KeyNotFoundException("Turma informada não encontrada.");

            var disciplina = await _subjectRepository.GetByIdAsync(dto.SubjectId)
                ?? throw new KeyNotFoundException("Disciplina informada não encontrada.");

            var professor = await _teacherRepository.GetByIdAsync(dto.TeacherId)
                ?? throw new KeyNotFoundException("Professor informado não encontrado.");

            var vinculoJaExiste = await _classSubjectRepository.ExistsAsync(dto.ClassId, dto.SubjectId);
            if (vinculoJaExiste)
                throw new InvalidOperationException("Esta turma já possui um professor cadastrado para esta disciplina.");

            var classSubject = new ClassSubject
            {
                ClassId = dto.ClassId,
                SubjectId = dto.SubjectId,
                TeacherId = dto.TeacherId
            };

            await _classSubjectRepository.AddAsync(classSubject);

            await _auditService.LogAsync(
                entityName: "ClassSubject",
                entityId: classSubject.Id,
                action: Actions.Created,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Vínculo criado: turma '{turma.Name}' + disciplina '{disciplina.Name}' + professor '{professor.Name}'."
            );

            classSubject.Class = turma;
            classSubject.Subject = disciplina;
            classSubject.Teacher = professor;

            return _mapper.Map<ClassSubjectResponseDto>(classSubject);
        }

        public async Task<ClassSubjectResponseDto> UpdateAsync(Guid id, UpdateClassSubjectDto dto, Guid performedBySecretaryId)
        {
            var classSubject = await _classSubjectRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Vínculo não encontrado.");

            var novoProfessor = await _teacherRepository.GetByIdAsync(dto.TeacherId)
                ?? throw new KeyNotFoundException("Professor informado não encontrado.");

            var professorAntigoId = classSubject.TeacherId;
            classSubject.TeacherId = dto.TeacherId;

            await _classSubjectRepository.UpdateAsync(classSubject);

            await _auditService.LogAsync(
                entityName: "ClassSubject",
                entityId: classSubject.Id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Professor do vínculo alterado para '{novoProfessor.Name}'."
            );

            classSubject.Teacher = novoProfessor;
            return _mapper.Map<ClassSubjectResponseDto>(classSubject);
        }

        public async Task DeleteAsync(Guid id, Guid performedBySecretaryId)
        {
            var classSubject = await _classSubjectRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Vínculo não encontrado.");

            await _classSubjectRepository.DeleteAsync(id);

            await _auditService.LogAsync(
                entityName: "ClassSubject",
                entityId: classSubject.Id,
                action: Actions.Deleted,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Vínculo (Turma/Disciplina/Professor) foi removido."
            );
        }

        public async Task<List<ClassSubjectResponseDto>> GetByTeacherIdAsync(Guid teacherId)
        {
            var allClassSubjects = await _classSubjectRepository.GetAllAsync();
            var myClassSubjects = allClassSubjects.Where(cs => cs.TeacherId == teacherId).ToList();

            return _mapper.Map<List<ClassSubjectResponseDto>>(myClassSubjects);
        }
    }
}