namespace School.Models
{
    public class ClassSubject : BaseEntity
    {

        public Class Class { get; set; }

        public Guid ClassId { get; set; }

        public Subject Subject { get; set; }

        public Guid SubjectId { get; set; }

        public Teacher Teacher { get; set; }

        public Guid TeacherId { get; set; }

        public ICollection<Grade> Grades { get; set; }

    }
}
