namespace School.Models
{
    public class Student : BaseEntity
    {

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string PasswordHash { get; set; }

        public string Registration { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid ClassId { get; set; }

        public Class Class { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Grade> Grades { get; set; }

    }
}
