using School.Dtos.SubjectDtos;
using School.DTOs;

namespace School.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<SubjectResponseDto>> GetAllAsync();

        Task<SubjectResponseDto?> GetByIdAsync(Guid id);

        Task<SubjectResponseDto> CreateAsync(CreateSubjectDto dto, Guid performedBySecretaryId);

        Task<SubjectResponseDto> UpdateAsync(Guid id, UpdateSubjectDto dto, Guid performedBySecretaryId);

        Task DeleteAsync(Guid id, Guid performedBySecretaryId);
    }
}