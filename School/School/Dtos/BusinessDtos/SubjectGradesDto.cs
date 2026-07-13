using School.Dtos.GradeDtos;

namespace School.Dtos.BusinessDtos
{
    public class SubjectGradesDto
    {
        public string SubjectName { get; set; }

        public string TeacherName { get; set; }

        public List<GradeResponseDto> Grades { get; set; } // uma por trimestre

    }
}
