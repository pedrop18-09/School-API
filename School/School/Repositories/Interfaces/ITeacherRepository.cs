using School.Models;

namespace School.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllAsync();

        Task<Teacher?> GetByIdAsync(Guid id);

        Task<Teacher?> GetByCpfAsync(string cpf);

        Task<List<Teacher>> GetInactiveAsync();

        Task<bool> ExistsByCpfAsync(string cpf);

        Task AddAsync(Teacher teacher);

        Task UpdateAsync(Teacher teacher);

        Task DeleteAsync(Guid id);

    }
}
