using AutoMapper;
using School.Dtos.BusinessDtos;
using School.Dtos.GradeDtos;
using School.DTOs;
using School.Enums;
using School.Models;
using School.Repositories.Interfaces;
using School.Services.Interfaces;

namespace School.Services
{
    public class GradeService : IGradeService
    {
        private const decimal PassingGrade = 6.0m;

        private readonly IGradeRepository _gradeRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassSubjectRepository _classSubjectRepository;
        private readonly IMapper _mapper;

        public GradeService(
            IGradeRepository gradeRepository,
            IStudentRepository studentRepository,
            IClassSubjectRepository classSubjectRepository,
            IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _studentRepository = studentRepository;
            _classSubjectRepository = classSubjectRepository;
            _mapper = mapper;
        }

        public async Task<List<GradeResponseDto>> GetAllAsync()
        {
            var grades = await _gradeRepository.GetAllAsync();
            return _mapper.Map<List<GradeResponseDto>>(grades);
        }

        public async Task<GradeResponseDto?> GetByIdAsync(Guid id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            if (grade == null) return null;

            grade.ClassSubject = await _classSubjectRepository.GetByIdAsync(grade.ClassSubjectId);
            return _mapper.Map<GradeResponseDto>(grade);
        }

        public async Task<GradeResponseDto> CreateAsync(CreateGradeDto dto)
        {
            if (!Enum.TryParse<Quarter>(dto.Quarter, ignoreCase: true, out var quarter))
                throw new ArgumentException($"Trimestre inválido: '{dto.Quarter}'.");

            var student = await _studentRepository.GetByIdAsync(dto.StudentId)
                ?? throw new KeyNotFoundException("Aluno não encontrado.");

            var classSubject = await _classSubjectRepository.GetByIdAsync(dto.ClassSubjectId)
                ?? throw new KeyNotFoundException("Vínculo turma/disciplina não encontrado.");

            // Garante que o aluno pertence à turma daquele vínculo
            if (student.ClassId != classSubject.ClassId)
                throw new InvalidOperationException("Este aluno não pertence à turma vinculada a esta disciplina.");

            var notaJaExiste = await _gradeRepository.ExistsAsync(dto.StudentId, dto.ClassSubjectId, quarter);
            if (notaJaExiste)
                throw new InvalidOperationException("Já existe uma nota lançada para este aluno, nesta disciplina, neste trimestre.");

            var grade = new Grade
            {
                StudentId = dto.StudentId,
                ClassSubjectId = dto.ClassSubjectId,
                Quarter = quarter,
                Exam1 = dto.Exam1,
                Exam2 = dto.Exam2,
                Assignment = dto.Assignment
            };

            await _gradeRepository.AddAsync(grade);

            grade.ClassSubject = classSubject;
            return _mapper.Map<GradeResponseDto>(grade);
        }

        public async Task<GradeResponseDto> UpdateAsync(Guid id, UpdateGradeDto dto)
        {
            var grade = await _gradeRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Nota não encontrada.");

            grade.Exam1 = dto.Exam1;
            grade.Exam2 = dto.Exam2;
            grade.Assignment = dto.Assignment;

            await _gradeRepository.UpdateAsync(grade);

            grade.ClassSubject = await _classSubjectRepository.GetByIdAsync(grade.ClassSubjectId);
            return _mapper.Map<GradeResponseDto>(grade);
        }

        public async Task DeleteAsync(Guid id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Nota não encontrada.");

            await _gradeRepository.DeleteAsync(id);
        }

        public async Task<StudentGradesDto> GetStudentGradesAsync(Guid studentId, int academicYear)
        {
            var student = await _studentRepository.GetByIdAsync(studentId)
                ?? throw new KeyNotFoundException("Aluno não encontrado.");

            var allGrades = await _gradeRepository.GetByStudentIdAsync(studentId);

            var gradesInYear = allGrades
                .Where(g => g.ClassSubject.Class.AcademicYear == academicYear)
                .ToList();

            var subjects = gradesInYear
                .GroupBy(g => g.ClassSubject.SubjectId)
                .Select(group => new SubjectGradesDto
                {
                    SubjectName = group.First().ClassSubject.Subject.Name,
                    TeacherName = group.First().ClassSubject.Teacher?.Name ?? "",
                    Grades = _mapper.Map<List<GradeResponseDto>>(group.ToList())
                })
                .ToList();

            return new StudentGradesDto
            {
                StudentName = student.Name,
                ClassName = student.Class?.Name ?? "",
                Subjects = subjects
            };
        }

        public async Task<List<int>> GetStudentAvailableYearsAsync(Guid studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId)
                ?? throw new KeyNotFoundException("Aluno não encontrado.");

            var allGrades = await _gradeRepository.GetByStudentIdAsync(studentId);

            var years = allGrades
                .Select(g => g.ClassSubject.Class.AcademicYear)
                .Distinct()
                .ToList();

            // Garante que o ano letivo ATUAL do aluno sempre aparece na lista,
            // mesmo que ele ainda não tenha nenhuma nota lançada nele
            if (student.Class != null && !years.Contains(student.Class.AcademicYear))
                years.Add(student.Class.AcademicYear);

            return years.OrderByDescending(y => y).ToList();
        }

        public async Task<TeacherClassPerformanceDto> GetClassPerformanceAsync(Guid classSubjectId, string quarterStr)
        {
            if (!Enum.TryParse<Quarter>(quarterStr, ignoreCase: true, out var quarter))
                throw new ArgumentException($"Trimestre inválido: '{quarterStr}'.");

            var classSubject = await _classSubjectRepository.GetByIdAsync(classSubjectId)
                ?? throw new KeyNotFoundException("Vínculo turma/disciplina não encontrado.");

            var allGrades = await _gradeRepository.GetByClassSubjectIdAsync(classSubjectId);
            var gradesInQuarter = allGrades.Where(g => g.Quarter == quarter).ToList();

            var studentAverages = gradesInQuarter
                .Select(g => new
                {
                    g.StudentId,
                    StudentName = g.Student.Name,
                    Average = (g.Exam1 + g.Exam2 + g.Assignment) / 3
                })
                .ToList();

            var classAverage = studentAverages.Count > 0
                ? studentAverages.Average(s => s.Average)
                : 0;

            var students = studentAverages
                .Select(s => new StudentPerformanceDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    Average = s.Average,
                    AboveAverage = s.Average > classAverage,
                    Approved = s.Average >= PassingGrade
                })
                .OrderByDescending(s => s.Average)
                .ToList();

            return new TeacherClassPerformanceDto
            {
                ClassName = classSubject.Class.Name,
                SubjectName = classSubject.Subject.Name,
                Quarter = quarter.ToString(),
                ClassAverage = classAverage,
                Students = students
            };
        }

        public async Task<GradeEntrySheetDto> GetEntrySheetAsync(Guid classSubjectId, string quarterStr, Guid teacherId)
        {
            if (!Enum.TryParse<Quarter>(quarterStr, ignoreCase: true, out var quarter))
                throw new ArgumentException($"Trimestre inválido: '{quarterStr}'.");

            var classSubject = await _classSubjectRepository.GetByIdAsync(classSubjectId)
                ?? throw new KeyNotFoundException("Vínculo turma/disciplina não encontrado.");

            if (classSubject.TeacherId != teacherId)
                throw new UnauthorizedAccessException("Você não leciona esta disciplina nesta turma.");

            var students = await _studentRepository.GetByClassIdAsync(classSubject.ClassId);
            var allGrades = await _gradeRepository.GetByClassSubjectIdAsync(classSubjectId);
            var gradesInQuarter = allGrades
                .Where(g => g.Quarter == quarter)
                .ToDictionary(g => g.StudentId);

            var entries = students.Select(s =>
            {
                gradesInQuarter.TryGetValue(s.Id, out var existingGrade);

                return new GradeEntryDto
                {
                    StudentId = s.Id,
                    StudentName = s.Name,
                    GradeId = existingGrade?.Id,
                    Exam1 = existingGrade?.Exam1,
                    Exam2 = existingGrade?.Exam2,
                    Assignment = existingGrade?.Assignment
                };
            }).ToList();

            return new GradeEntrySheetDto
            {
                ClassName = classSubject.Class.Name,
                SubjectName = classSubject.Subject.Name,
                Quarter = quarter.ToString(),
                Students = entries
            };
        }
    }
}