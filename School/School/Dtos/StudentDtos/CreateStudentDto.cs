namespace School.Dtos.StudentDtos
{
    public class CreateStudentDto
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Password { get; set; }

        public string Registration { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid ClassId { get; set; }

    }
}
