using AutoMapper;
using School.DTOs;
using School.Models;
using School.Enums;
using School.Repositories.Interfaces;
using School.Services.Interfaces;

namespace School.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IMapper _mapper;

        public AuditService(IAuditLogRepository auditLogRepository, IMapper mapper)
        {
            _auditLogRepository = auditLogRepository;
            _mapper = mapper;
        }

        public async Task LogAsync(string entityName, Guid entityId, Actions action, Guid performedBySecretaryId, string details)
        {
            var log = new AuditLog
            {
                EntityName = entityName,
                EntityId = entityId,
                Actions = action,
                PerformedBySecretaryId = performedBySecretaryId,
                TimeStamp = DateTime.UtcNow,
                Details = details
            };

            await _auditLogRepository.AddAsync(log);
        }

        public async Task<List<AuditLogResponseDto>> GetAllAsync()
        {
            var logs = await _auditLogRepository.GetAllAsync();
            return _mapper.Map<List<AuditLogResponseDto>>(logs);
        }

        public async Task<List<AuditLogResponseDto>> GetByEntityNameAsync(string entityName)
        {
            var logs = await _auditLogRepository.GetByEntityNameAsync(entityName);
            return _mapper.Map<List<AuditLogResponseDto>>(logs);
        }

        public async Task<List<AuditLogResponseDto>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            var logs = await _auditLogRepository.GetByDateRangeAsync(start, end);
            return _mapper.Map<List<AuditLogResponseDto>>(logs);
        }
    }
}