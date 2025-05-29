using FCG.Application.Interfaces;
using FCG.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FCG.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _admineService;

        public AdminController(IAdminService AdminService)
        {
            _admineService = AdminService;
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("CreateGame")]
        public async Task<IActionResult> CreateGame(CreateGameRequest createGameResquest)
        {
            var game = await _admineService.CreateGameAsync(createGameResquest);

            return Ok(game);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("UpdateUser")]

        public async Task<IActionResult> UpdateUser(UpdateUserRequest updateUserResquest)
        {
            var user = await _admineService.UpdateUserAsync(updateUserResquest);

            if(user.Exists)
                return Ok(user);
            else
                throw new ArgumentException("Usuário não encontrado");
        }

    }
}
