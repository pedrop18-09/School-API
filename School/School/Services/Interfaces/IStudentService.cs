using School.Dtos.StudentDtos;

namespace School.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentResponseDto>> GetAllAsync();

        Task<StudentResponseDto?> GetByIdAsync(Guid id);

        Task<List<StudentResponseDto>> GetInactiveAsync();

        Task<StudentResponseDto> ReactivateAsync(Guid id, Guid performedBySecretaryId);

        Task<StudentResponseDto> CreateAsync(CreateStudentDto dto, Guid performedBySecretaryId);

        Task<StudentResponseDto> UpdateAsync(Guid id, UpdateStudentDto dto, Guid performedBySecretaryId);

        Task DeleteAsync(Guid id, Guid performedBySecretaryId);
    }
}
