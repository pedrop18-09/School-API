using School.Dtos.SecretaryDtos;
using School.DTOs;

namespace School.Services.Interfaces
{
    public interface ISecretaryService
    {
        Task<List<SecretaryResponseDto>> GetAllAsync();

        Task<SecretaryResponseDto?> GetByIdAsync(Guid id);

        Task<SecretaryResponseDto> UpdateAsync(Guid id, UpdateSecretaryDto dto, Guid performedBySecretaryId);

        Task DeleteAsync(Guid id, Guid performedBySecretaryId);
    }
}