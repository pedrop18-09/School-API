namespace School.Models
{
    public class Secretary : BaseEntity
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<AuditLog> AuditLogs { get; set; } = [];

    }
}
