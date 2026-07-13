using School.Models;

namespace School.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllAsync();

        Task<Subject?> GetByIdAsync(Guid id);

        Task<bool> ExistsByNameAsync(string name);

        Task AddAsync(Subject subject);

        Task UpdateAsync(Subject subject);

        Task DeleteAsync(Guid id);
    }
}