namespace School.Dtos.StudentDtos
{
    public class UpdateStudentDto
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid ClassId { get; set; }

    }
}
