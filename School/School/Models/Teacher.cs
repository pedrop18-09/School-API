namespace School.Models
{
    public class Teacher : BaseEntity
    {

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<ClassSubject> ClassSubjects { get; set; }

    }
}
