using School.Dtos.ClassDtos;
using School.DTOs;

namespace School.Services.Interfaces
{
    public interface IClassService
    {
        Task<List<ClassResponseDto>> GetAllAsync();

        Task<ClassResponseDto?> GetByIdAsync(Guid id);

        Task<ClassResponseDto> CreateAsync(CreateClassDto dto, Guid performedBySecretaryId);

        Task<ClassResponseDto> UpdateAsync(Guid id, UpdateClassDto dto, Guid performedBySecretaryId);

        Task DeleteAsync(Guid id, Guid performedBySecretaryId);
    }
}