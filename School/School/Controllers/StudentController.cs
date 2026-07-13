using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Dtos.StudentDtos;
using School.Services.Interfaces;

namespace School.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = "Secretary")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var students = await _studentService.GetAllAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Secretary")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var student = await _studentService.GetByIdAsync(id);

                if (student == null)
                    return NotFound("Aluno não encontrado.");

                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Secretary")]
        [HttpGet("inactive")]
        public async Task<IActionResult> GetInactive()
        {
            try
            {
                var students = await _studentService.GetInactiveAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Secretary")]
        [HttpPut("{id}/reactivate")]
        public async Task<IActionResult> Reactivate(Guid id)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var student = await _studentService.ReactivateAsync(id, secretaryId);
                return Ok(student);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Secretary")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var student = await _studentService.CreateAsync(dto, secretaryId);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = student.Id },
                    student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Secretary")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateStudentDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var student = await _studentService.UpdateAsync(id, dto, secretaryId);

                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Secretary")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                await _studentService.DeleteAsync(id, secretaryId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}