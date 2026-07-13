using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Dtos.ClassDtos;
using School.Services.Interfaces;

namespace School.Controllers
{
    [ApiController]
    [Route("api/class")]
    [Authorize(Roles = "Secretary")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var classes = await _classService.GetAllAsync();
                return Ok(classes);
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
                var @class = await _classService.GetByIdAsync(id);

                if (@class == null)
                    return NotFound("Turma não encontrada.");

                return Ok(@class);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClassDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var @class = await _classService.CreateAsync(dto, secretaryId);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = @class.Id },
                    @class);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateClassDto dto)
        {
            try
            {
                var secretaryId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var @class = await _classService.UpdateAsync(id, dto, secretaryId);

                return Ok(@class);
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

                await _classService.DeleteAsync(id, secretaryId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}