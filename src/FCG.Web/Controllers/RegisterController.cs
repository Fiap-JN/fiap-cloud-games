using FCG.Application.Interfaces;
using FCG.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace FCG.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserRequest createUserRequest)
        {
            var user = await _userService.CreateUserAsync(createUserRequest);

            return Ok(user);
        }
    }
}
