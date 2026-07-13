using School.DTOs;
using School.Enums;

namespace School.Services.Interfaces
{
    public interface IAuditService
    {
        // Usado pelos outros Services para registrar uma ação
        Task LogAsync(string entityName, Guid entityId, Actions action, Guid performedBySecretaryId, string details);

        // Usados pelo AuditController, para exibir o painel da Secretaria
        Task<List<AuditLogResponseDto>> GetAllAsync();

        Task<List<AuditLogResponseDto>> GetByEntityNameAsync(string entityName);

        Task<List<AuditLogResponseDto>> GetByDateRangeAsync(DateTime start, DateTime end);
    }
}