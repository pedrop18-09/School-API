using School.Dtos.AudthDtos;
using School.DTOs;
using School.Repositories.Interfaces;
using School.Services.Interfaces;

namespace School.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            ISecretaryRepository secretaryRepository,
            ITeacherRepository teacherRepository,
            IStudentRepository studentRepository,
            ITokenService tokenService)
        {
            _secretaryRepository = secretaryRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
            _tokenService = tokenService;
        }

        // MÉTODO PRIVADO
        private AuthResponseDto CreateAuthResponse(Guid id, string name, string role)
        {
            var token = _tokenService.GenerateToken(id, name, role);

            return new AuthResponseDto
            {
                Token = token,
                Name = name,
                Role = role
            };
        }

        public async Task<AuthResponseDto?> LoginSecretaryAsync(LoginSecretaryDto dto)
        {
            var secretary = await _secretaryRepository.GetByEmailAsync(dto.Email);

            if (secretary == null || !BCrypt.Net.BCrypt.Verify(dto.Password, secretary.PasswordHash))
                return null;

            return CreateAuthResponse(secretary.Id, secretary.Name, "Secretary");
        }

        public async Task<AuthResponseDto?> LoginTeacherAsync(TeacherStudentLoginDto dto)
        {
            var teacher = await _teacherRepository.GetByCpfAsync(dto.Cpf);

            if (teacher == null || !BCrypt.Net.BCrypt.Verify(dto.Password, teacher.PasswordHash))
                return null;

            if (!teacher.IsActive)
                return null; // trata como "credenciais inválidas" — não revela que a conta existe mas está desativada

            var token = _tokenService.GenerateToken(teacher.Id, teacher.Name, "Teacher");

            return new AuthResponseDto
            {
                Token = token,
                Name = teacher.Name,
                Role = "Teacher"
            };
        }

        public async Task<AuthResponseDto?> LoginStudentAsync(TeacherStudentLoginDto dto)
        {
            var student = await _studentRepository.GetByCpfAsync(dto.Cpf);

            if (student == null || !BCrypt.Net.BCrypt.Verify(dto.Password, student.PasswordHash))
                return null;

            if (!student.IsActive)
                return null; // mesma lógica do Teacher: trata como credenciais inválidas

            var token = _tokenService.GenerateToken(student.Id, student.Name, "Student");

            return new AuthResponseDto
            {
                Token = token,
                Name = student.Name,
                Role = "Student"
            };
        }
    }
}