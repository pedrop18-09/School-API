using School.Models;
using School.Enums;

namespace School.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        Task<List<Grade>> GetAllAsync();

        Task<Grade?> GetByIdAsync(Guid id);

        Task<List<Grade>> GetByStudentIdAsync(Guid studentId);

        Task<List<Grade>> GetByClassSubjectIdAsync(Guid classSubjectId);

        Task<bool> ExistsAsync(Guid studentId, Guid classSubjectId, Quarter quarter);

        Task AddAsync(Grade grade);

        Task UpdateAsync(Grade grade);

        Task DeleteAsync(Guid id);
    }
}