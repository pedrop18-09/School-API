using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Dtos.AudthDtos;
using School.DTOs;
using School.Services.Interfaces;

namespace School.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("secretary/login")]
        public async Task<ActionResult<AuthResponseDto>> LoginSecretary(LoginSecretaryDto dto)
        {
            try
            {
                var result = await _authService.LoginSecretaryAsync(dto);

                if (result == null)
                    return Unauthorized("Email ou senha inválidos.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("teacher/login")]
        public async Task<ActionResult<AuthResponseDto>> LoginTeacher(TeacherStudentLoginDto dto)
        {
            try
            {
                var result = await _authService.LoginTeacherAsync(dto);

                if (result == null)
                    return Unauthorized("CPF ou senha inválidos.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("student/login")]
        public async Task<ActionResult<AuthResponseDto>> LoginStudent(TeacherStudentLoginDto dto)
        {
            try
            {
                var result = await _authService.LoginStudentAsync(dto);

                if (result == null)
                    return Unauthorized("CPF ou senha inválidos.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}