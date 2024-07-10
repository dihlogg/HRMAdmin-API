using Microsoft.AspNetCore.Mvc;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Services;

namespace AdminHRM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeServive _employeeService;

        public EmployeeController(
            ILogger<EmployeeController> logger,
            IEmployeeServive employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var data = await _employeeService.GetEmployeeDtosAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostEmployee")]
        public async Task<IActionResult> PostEmployee(EmployeeCreateDto employeeCreateDto)
        {
            try
            {
                var data = await _employeeService.AddEmployeeAsync(employeeCreateDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutEmployee")]
        public async Task<IActionResult> PutEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var data = await _employeeService.EditEmployeeAsync(employeeDto);
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

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var data = await _employeeService.RemoveEmployeeDtosAsync(id);
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

        [HttpGet("SearchEmployees")]
        public async Task<IActionResult> SearchEmployees(
            string? employeeName = null,
            string? status = null,
            string? jobTitle = null,
            string? supervisorName = null,
            string? subName = null)
        {
            try
            {
                var data = await _employeeService.SearchEmployeeDtosAsync(employeeName, status, jobTitle, supervisorName, subName);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPagingRecord")]
        public async Task<ActionResult<PagedResult<EmployeeDto>>> GetEmployees(int page = 1, int pageSize = 10)
        {
            var pagedResult = await _employeeService.GetPagedEmployeesAsync(page, pageSize);
            return Ok(pagedResult);
        }
    }
}