using School.Data;
using School.Models;
using School.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace School.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _context.AuditLogs
                .Include(a => a.PerformedBySecretary)
                .OrderByDescending(a => a.TimeStamp)
                .ToListAsync();
        }

        public async Task<List<AuditLog>> GetByEntityNameAsync(string entityName)
        {
            return await _context.AuditLogs
                .Include(a => a.PerformedBySecretary)
                .Where(a => a.EntityName == entityName)
                .OrderByDescending(a => a.TimeStamp)
                .ToListAsync();
        }

        public async Task<List<AuditLog>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.AuditLogs
                .Include(a => a.PerformedBySecretary)
                .Where(a => a.TimeStamp >= start && a.TimeStamp <= end)
                .OrderByDescending(a => a.TimeStamp)
                .ToListAsync();
        }

        public async Task AddAsync(AuditLog auditLog)
        {
            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
        }
    }
}