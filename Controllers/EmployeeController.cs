using Microsoft.AspNetCore.Mvc;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Services;
using AdminHRM.Dtos;

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
        public async Task<IActionResult> SearchEmployees(SearchEmployeeDto searchEmployeeDto)
        {
            try
            {
                var data = await _employeeService.SearchEmployeeDtosAsync(searchEmployeeDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPagingRecord")]
        public async Task<ActionResult<PagedResult<EmployeeDto>>> GetPagedEmployees([FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] string sortFields,
            [FromQuery] string sortOrders)
        {
            var sortFieldArray = sortFields.Split(',');
            var sortOrderArray = sortOrders.Split(',');

            if (sortFieldArray.Length != sortOrderArray.Length)
            {
                return BadRequest();
            }

            var result = await _employeeService.GetPagedEmployeesAsync(page, pageSize, sortFieldArray, sortOrderArray);
            return Ok(result);
        }
    }
}