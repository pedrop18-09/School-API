namespace School.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } // "Secretary", "Teacher" ou "Student"
    }
}