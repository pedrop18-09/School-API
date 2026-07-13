namespace School.Dtos.GradeDtos
{
    public class GradeEntryDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public Guid? GradeId { get; set; }      // null = ainda não foi lançada nota nesse trimestre
        public decimal? Exam1 { get; set; }
        public decimal? Exam2 { get; set; }
        public decimal? Assignment { get; set; }
    }
}
