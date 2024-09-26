using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHRM.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet("Employees")]
        public IEnumerable<string> Get()
        {
            return new List<string> { "Long1", "Long2", "long3" };
        }
    }
}
