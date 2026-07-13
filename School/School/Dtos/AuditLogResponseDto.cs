namespace School.DTOs
{
    public class AuditLogResponseDto
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public Guid EntityId { get; set; }
        public string Action { get; set; }
        public string PerformedBySecretaryName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
    }
}