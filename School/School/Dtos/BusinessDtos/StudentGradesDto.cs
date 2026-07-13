
namespace School.Dtos.BusinessDtos
{
    // Visão do Aluno: todas as disciplinas com suas notas por trimestre
    public class StudentGradesDto
    {
        public string StudentName { get; set; }

        public string ClassName { get; set; }

        public List<SubjectGradesDto> Subjects { get; set; }

    }
}
