using School.Models;

namespace School.Repositories.Interfaces
{
    public interface IClassSubjectRepository
    {
        Task<List<ClassSubject>> GetAllAsync();

        Task<ClassSubject?> GetByIdAsync(Guid id);

        Task<ClassSubject?> GetByClassAndSubjectAsync(Guid classId, Guid subjectId);

        Task<bool> ExistsAsync(Guid classId, Guid subjectId);

        Task AddAsync(ClassSubject classSubject);
            
        Task UpdateAsync(ClassSubject classSubject);

        Task DeleteAsync(Guid id);
    }
}
