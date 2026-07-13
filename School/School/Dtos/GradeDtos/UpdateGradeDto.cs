namespace School.Dtos.GradeDtos
{
    public class UpdateGradeDto
    {
        public decimal Exam1 { get; set; }

        public decimal Exam2 { get; set; }

        public decimal Assignment { get; set; }
        // Student, ClassSubject e Trimester não são editáveis;
        // pra mudar isso, apaga e cria uma nova nota

    }
}
