namespace School.Dtos.GradeDtos
{
    public class GradeResponseDto
    {
        public Guid Id { get; set; }

        public string SubjectName { get; set; }

        public string Quarter { get; set; }

        public decimal Exam1 { get; set; }

        public decimal Exam2 { get; set; }

        public decimal Assignment { get; set; }

        public decimal Average { get; set; } // calculada, nunca vem do banco

    }
}
