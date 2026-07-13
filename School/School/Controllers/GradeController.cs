using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Dtos.GradeDtos;
using School.Services.Interfaces;

namespace School.Controllers
{
    [ApiController]
    [Route("api/grade")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        // ===================== SECRETARY =====================

        [Authorize(Roles = "Secretary")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var grades = await _gradeService.GetAllAsync();
                return Ok(grades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ===================== SECRETARY / TEACHER =====================

        [Authorize(Roles = "Secretary,Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var grade = await _gradeService.GetByIdAsync(id);

                if (grade == null)
                    return NotFound("Nota não encontrada.");

                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ===================== TEACHER =====================

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGradeDto dto)
        {
            try
            {
                var grade = await _gradeService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = grade.Id },
                    grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateGradeDto dto)
        {
            try
            {
                var grade = await _gradeService.UpdateAsync(id, dto);

                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _gradeService.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ===================== STUDENT =====================

        [Authorize(Roles = "Student")]
        [HttpGet("my-grades")]
        public async Task<IActionResult> GetMyGrades([FromQuery] int year)
        {
            try
            {
                var studentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var grades = await _gradeService.GetStudentGradesAsync(studentId, year);

                return Ok(grades);
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

        [Authorize(Roles = "Student")]
        [HttpGet("my-years")]
        public async Task<IActionResult> GetMyYears()
        {
            try
            {
                var studentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var years = await _gradeService.GetStudentAvailableYearsAsync(studentId);

                return Ok(years);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ===================== TEACHER =====================

        [Authorize(Roles = "Teacher")]
        [HttpGet("class-performance/{classSubjectId}")]
        public async Task<IActionResult> GetClassPerformance(Guid classSubjectId, [FromQuery] string quarter)
        {
            try
            {
                var performance = await _gradeService.GetClassPerformanceAsync(classSubjectId, quarter);

                return Ok(performance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Adicionar esse método dentro da classe GradeController,
        // junto com os outros endpoints de TEACHER

        [Authorize(Roles = "Teacher")]
        [HttpGet("entry-sheet/{classSubjectId}")]
        public async Task<IActionResult> GetEntrySheet(Guid classSubjectId, [FromQuery] string quarter)
        {
            try
            {
                var teacherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var sheet = await _gradeService.GetEntrySheetAsync(classSubjectId, quarter, teacherId);

                return Ok(sheet);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}