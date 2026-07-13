using School.Models;

namespace School.Repositories.Interfaces
{
    public interface ISecretaryRepository
    {
        Task<List<Secretary>> GetAllAsync();

        Task<Secretary?> GetByIdAsync(Guid id);

        Task<Secretary?> GetByEmailAsync(string email);

        Task<bool> ExistsByEmailAsync(string email);

        Task<bool> ExistsByCpfAsync(string cpf);

        Task AddAsync(Secretary secretary);

        Task UpdateAsync(Secretary secretary);

        Task DeleteAsync(Guid id);

    }
}
