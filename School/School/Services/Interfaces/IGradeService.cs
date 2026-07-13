using School.Dtos.BusinessDtos;
using School.Dtos.GradeDtos;
using School.DTOs;

namespace School.Services.Interfaces
{
    public interface IGradeService
    {
        Task<List<GradeResponseDto>> GetAllAsync();

        Task<GradeResponseDto?> GetByIdAsync(Guid id);

        Task<GradeResponseDto> CreateAsync(CreateGradeDto dto);

        Task<GradeResponseDto> UpdateAsync(Guid id, UpdateGradeDto dto);

        Task DeleteAsync(Guid id);

        // Visão do Aluno: todas as disciplinas + notas
        Task<StudentGradesDto> GetStudentGradesAsync(Guid studentId, int academicYear);

        Task<List<int>> GetStudentAvailableYearsAsync(Guid studentId);

        // Visão do Professor: desempenho da turma numa disciplina/trimestre
        Task<TeacherClassPerformanceDto> GetClassPerformanceAsync(Guid classSubjectId, string quarter);

        // Visão do Professor: folha de lançamento de notas (alunos + nota existente, se houver)
        Task<GradeEntrySheetDto> GetEntrySheetAsync(Guid classSubjectId, string quarter, Guid teacherId);
    }
}