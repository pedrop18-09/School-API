using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Dtos.TeacherDtos;
using School.Services.Interfaces;

namespace School.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    [Authorize(Roles = "Secretary")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var teachers = await _teacherService.GetAllAsync();
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var teacher = await _teacherService.GetByIdAsync(id);

                if (teacher == null)
                    return NotFound("Professor não encontrado.");

                return Ok(teacher);
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
                var teachers = await _teacherService.GetInactiveAsync();
                return Ok(teachers);
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
                var teacher = await _teacherService.ReactivateAsync(id, secretaryId);
                return Ok(teacher);
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeacherDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var teacher = await _teacherService.CreateAsync(dto, secretaryId);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = teacher.Id },
                    teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTeacherDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var teacher = await _teacherService.UpdateAsync(id, dto, secretaryId);

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                await _teacherService.DeleteAsync(id, secretaryId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}