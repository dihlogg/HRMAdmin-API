using Microsoft.AspNetCore.Mvc;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Services;
using AdminHRM.Services.Implements;
using AdminHRM.Dtos.Leaves;

namespace AdminHRM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILogger<LeaveController> _logger;
        private readonly ILeaveServive _leaveService;

        public LeaveController(
            ILogger<LeaveController> logger,
            ILeaveServive leaveService)
        {
            _logger = logger;
            _leaveService = leaveService;
        }

        [HttpGet("GetLeaves")]
        public async Task<IActionResult> GetLeaves()
        {
            try
            {
                var data = await _leaveService.GetLeaveDtosAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostLeave")]
        public async Task<IActionResult> PostLeave(AdminHRM.Dtos.Leaves.LeaveCreateDto leaveCreateDto)
        {
            try
            {
                var data = await _leaveService.AddLeaveAsync(leaveCreateDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutLeave")]
        public async Task<IActionResult> PutLeave(LeaveDto leaveDto)
        {
            try
            {
                var data = await _leaveService.EditLeaveAsync(leaveDto);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteLeave/{id}")]
        public async Task<IActionResult> DeleteLeave(Guid id)
        {
            try
            {
                var data = await _leaveService.RemoveLeaveDtosAsync(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchLeaves")]
        public async Task<IActionResult> SearchLeaves([FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] string? leaveType,
            [FromQuery] string? leaveStatus,
            [FromQuery] string? employeeName,
            [FromQuery] string? subName)
        {
            try
            {
                var searchLeaveDto = new SearchLeaveDto
                {
                    FromDate = fromDate,
                    ToDate = toDate,
                    LeaveType = leaveType,
                    LeaveStatus = leaveStatus,
                    EmployeeName = employeeName,
                    SubName = subName
                };
                var data = await _leaveService.SearchLeaveDtosAsync(searchLeaveDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}