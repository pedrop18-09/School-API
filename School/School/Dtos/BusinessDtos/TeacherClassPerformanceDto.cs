namespace School.Dtos.BusinessDtos
{
    // Visão do Professor: performance dos alunos numa turma/disciplina específica
    public class TeacherClassPerformanceDto
    {
        public string ClassName { get; set; }

        public string SubjectName { get; set; }

        public string Quarter { get; set; }

        public decimal ClassAverage { get; set; }

        public List<StudentPerformanceDto> Students { get; set; }

    }
}
