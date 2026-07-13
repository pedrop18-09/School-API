using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Services.Interfaces;

namespace School.Controllers
{
    [ApiController]
    [Route("api/audit")]
    [Authorize(Roles = "Secretary")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var logs = await _auditService.GetAllAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("entity/{entityName}")]
        public async Task<IActionResult> GetByEntityName(string entityName)
        {
            try
            {
                var logs = await _auditService.GetByEntityNameAsync(entityName);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("date-range")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            try
            {
                if (start > end)
                    return BadRequest("A data inicial não pode ser maior que a data final.");

                var logs = await _auditService.GetByDateRangeAsync(start, end);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}