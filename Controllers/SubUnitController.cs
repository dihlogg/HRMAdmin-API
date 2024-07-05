using Microsoft.AspNetCore.Mvc;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Services;

namespace AdminHRM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubUnitController : ControllerBase
    {
        private readonly ILogger<SubUnitController> _logger;
        private readonly ISubUnitService _subUnitService;

        public SubUnitController(
            ILogger<SubUnitController> logger,
            ISubUnitService subUnitService)
        {
            _logger = logger;
            _subUnitService = subUnitService;
        }

        [HttpGet("GetSubUnits")]
        public async Task<IActionResult> GetSubUnits()
        {
            try
            {
                var data = await _subUnitService.GetSubUnitDtosAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostSubUnit")]
        public async Task<IActionResult> PostSubUnit(SubUnitCreateDto subUnitCreateDto)
        {
            try
            {
                var data = await _subUnitService.AddSubUnitAsync(subUnitCreateDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutSubUnit")]
        public async Task<IActionResult> PutSubUnit(SubUnitDto subUnitDto)
        {
            try
            {
                var data = await _subUnitService.EditSubUnitAsync(subUnitDto);
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

        [HttpDelete("DeleteSubUnit/{id}")]
        public async Task<IActionResult> DeleteSubUnit(Guid id)
        {
            try
            {
                var data = await _subUnitService.RemoveSubUnitDtosAsync(id);
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
    }
}