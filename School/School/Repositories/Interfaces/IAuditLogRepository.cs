using School.Models;

namespace School.Repositories.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<List<AuditLog>> GetAllAsync();

        Task<List<AuditLog>> GetByEntityNameAsync(string entityName);

        Task<List<AuditLog>> GetByDateRangeAsync(DateTime start, DateTime end);

        Task AddAsync(AuditLog auditLog);
    }
}