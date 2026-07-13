using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Dtos.ClassSubjectDtos;
using School.Services.Interfaces;

namespace School.Controllers
{
    [ApiController]
    [Route("api/class-subject")]
    public class ClassSubjectController : ControllerBase
    {
        private readonly IClassSubjectService _classSubjectService;

        public ClassSubjectController(IClassSubjectService classSubjectService)
        {
            _classSubjectService = classSubjectService;
        }


        [Authorize(Roles = "Secretary")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var classSubjects = await _classSubjectService.GetAllAsync();
                return Ok(classSubjects);
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
                var classSubject = await _classSubjectService.GetByIdAsync(id);

                if (classSubject == null)
                    return NotFound("Relação turma-disciplina não encontrada.");

                return Ok(classSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Secretary")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassSubjectDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var classSubject = await _classSubjectService.CreateAsync(dto, secretaryId);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = classSubject.Id },
                    classSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Secretary")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateClassSubjectDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var classSubject = await _classSubjectService.UpdateAsync(id, dto, secretaryId);

                return Ok(classSubject);
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

                await _classSubjectService.DeleteAsync(id, secretaryId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyClassSubjects()
        {
            try
            {
                var teacherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var classSubjects = await _classSubjectService.GetByTeacherIdAsync(teacherId);

                return Ok(classSubjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}