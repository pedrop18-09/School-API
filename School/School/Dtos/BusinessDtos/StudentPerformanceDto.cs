namespace School.Dtos.BusinessDtos
{
    public class StudentPerformanceDto
    {
        public Guid StudentId { get; set; }

        public string StudentName { get; set; }

        public decimal Average { get; set; }

        public bool AboveAverage { get; set; } // true = acima da média da turma

        public bool Approved { get; set; }

    }
}
