using School.Dtos.TeacherDtos;
using School.DTOs;

namespace School.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherResponseDto>> GetAllAsync();

        Task<TeacherResponseDto?> GetByIdAsync(Guid id);

        Task<List<TeacherResponseDto>> GetInactiveAsync();

        Task<TeacherResponseDto> ReactivateAsync(Guid id, Guid performedBySecretaryId);

        Task<TeacherResponseDto> CreateAsync(CreateTeacherDto dto, Guid performedBySecretaryId);

        Task<TeacherResponseDto> UpdateAsync(Guid id, UpdateTeacherDto dto, Guid performedBySecretaryId);

        Task DeleteAsync(Guid id, Guid performedBySecretaryId);
    }
}