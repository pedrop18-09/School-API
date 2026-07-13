using School.Models;

namespace School.Repositories.Interfaces
{
    public interface IClassRepository
    {
        Task<List<Class>> GetAllAsync();

        Task<Class?> GetByIdAsync(Guid id);

        Task AddAsync(Class @class);

        Task UpdateAsync(Class @class);

        Task DeleteAsync(Guid id);

    }
}
