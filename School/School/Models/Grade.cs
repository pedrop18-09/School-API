using School.Enums;

namespace School.Models
{
    public class Grade : BaseEntity
    {
        public Student Student { get; set; }

        public Guid StudentId { get; set; }

        public ClassSubject ClassSubject { get; set; }

        public Guid ClassSubjectId { get; set; }

        public Quarter Quarter { get; set; }

        public decimal Exam1 { get; set; }
        
        public decimal Exam2 { get; set; }

        public decimal Assignment { get; set; }

        public double Average { get; set; }

        public DateTime ReleaseDate { get; set; }

    }
}
