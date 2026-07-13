using School.Models;

namespace School.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();

        Task<Student?> GetByIdAsync(Guid id);

        Task<List<Student>> GetInactiveAsync();

        Task<Student?> GetByCpfAsync(string cpf);

        Task<bool> ExistsByCpfAsync(string cpf);

        Task<bool> ExistsByRegistrationAsync(string registration);

        Task<List<Student>> GetByClassIdAsync(Guid classId);

        Task AddAsync(Student student);

        Task UpdateAsync(Student student);

        Task DeleteAsync(Guid id);
    }
}