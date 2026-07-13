using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Dtos.SubjectDtos;
using School.Services.Interfaces;

namespace School.Controllers
{
    [ApiController]
    [Route("api/subject")]
    [Authorize(Roles = "Secretary")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var subjects = await _subjectService.GetAllAsync();
                return Ok(subjects);
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
                var subject = await _subjectService.GetByIdAsync(id);

                if (subject == null)
                    return NotFound("Disciplina não encontrada.");

                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubjectDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var subject = await _subjectService.CreateAsync(dto, secretaryId);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = subject.Id },
                    subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateSubjectDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var subject = await _subjectService.UpdateAsync(id, dto, secretaryId);

                return Ok(subject);
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

                await _subjectService.DeleteAsync(id, secretaryId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}