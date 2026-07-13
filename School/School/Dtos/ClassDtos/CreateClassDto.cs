namespace School.Dtos.ClassDtos
{
    public class CreateClassDto
    {
        public string Name { get; set; }

        public string SchoolGrade { get; set; } // recebido como string, convertido pro enum no service/controller

        public int AcademicYear { get; set; }

    }
}
