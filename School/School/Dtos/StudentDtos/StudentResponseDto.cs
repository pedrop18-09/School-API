namespace School.Dtos.StudentDtos
{
    public class StudentResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Registration { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ClassName { get; set; }

        public Guid ClassId { get; set; }

    }
}
