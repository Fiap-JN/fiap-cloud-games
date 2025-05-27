using Microsoft.AspNetCore.Mvc;

namespace FCG.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("CreateGame")]
        public async Task<IActionResult> CreateGame()
        {
            return Ok();
        }
    }
}
