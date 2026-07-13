namespace School.Models
{
    public class Subject : BaseEntity
    {

        public string Name { get; set; }

        public ICollection<ClassSubject> ClassSubjects { get; set; }

    }
}
