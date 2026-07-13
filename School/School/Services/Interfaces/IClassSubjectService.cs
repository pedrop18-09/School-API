using School.Dtos.ClassSubjectDtos;
using School.DTOs;

namespace School.Services.Interfaces
{
    public interface IClassSubjectService
    {
        Task<List<ClassSubjectResponseDto>> GetAllAsync();

        Task<ClassSubjectResponseDto?> GetByIdAsync(Guid id);

        Task<ClassSubjectResponseDto> CreateAsync(CreateClassSubjectDto dto, Guid performedBySecretaryId);

        Task<ClassSubjectResponseDto> UpdateAsync(Guid id, UpdateClassSubjectDto dto, Guid performedBySecretaryId);

        Task DeleteAsync(Guid id, Guid performedBySecretaryId);

        // Usado pelo Professor, para ver apenas os vínculos onde ele leciona
        Task<List<ClassSubjectResponseDto>> GetByTeacherIdAsync(Guid teacherId);
    }
}