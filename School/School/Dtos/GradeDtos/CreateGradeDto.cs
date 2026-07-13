namespace School.Dtos.GradeDtos
{
    public class CreateGradeDto
    {
        public Guid StudentId { get; set; }

        public Guid ClassSubjectId { get; set; }

        public string Quarter { get; set; } // string recebida, convertida pro enum no service

        public decimal Exam1 { get; set; }

        public decimal Exam2 { get; set; }

        public decimal Assignment { get; set; }

    }
}
