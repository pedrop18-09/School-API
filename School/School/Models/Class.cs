using School.Enums;

namespace School.Models
{
    public class Class : BaseEntity
    {

        public string Name { get; set; }

        public SchoolGrade SchoolGrade { get; set; }

        public int AcademicYear { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<ClassSubject> ClassSubjects { get; set; }

    }
}
