using School.Enums;

namespace School.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; }

        public string EntityName { get; set; }

        public Guid EntityId { get; set; }

        public Actions Actions { get; set; }

        public Guid PerformedBySecretaryId { get; set; }

        public Secretary PerformedBySecretary { get; set; } = null!;

        public DateTime TimeStamp { get; set; }

        public string Details { get; set; }

    }
}
