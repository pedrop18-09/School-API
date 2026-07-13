using School.Dtos.AudthDtos;
using School.DTOs;

namespace School.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginSecretaryAsync(LoginSecretaryDto dto);

        Task<AuthResponseDto?> LoginTeacherAsync(TeacherStudentLoginDto dto);

        Task<AuthResponseDto?> LoginStudentAsync(TeacherStudentLoginDto dto);
    }
}