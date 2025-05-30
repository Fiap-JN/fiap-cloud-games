using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        [HttpPost("PromoteUser/{userId}")]
        public async Task<IActionResult> PromoteUser(int userId)
        {
            try
            {
                await _adminService.PromoteUserAsync(userId);
                _logger.LogInformation($"Usuário {userId} promovido a admin.");
                return Ok($"Usuário {userId} promovido a admin.");
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Usuário não encontrado: {userId}");
                return NotFound("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao promover usuário {userId}.");
                return StatusCode(500, "Erro interno ao promover usuário.");
            }
        }

        [HttpPost("BanUser/{userId}")]
        public async Task<IActionResult> BanUser(int userId)
        {
            try
            {
                await _adminService.BanUserAsync(userId);

                _logger.LogInformation($"Usuário {userId} banido.");
                return Ok($"Usuário {userId} banido.");
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Usuário não encontrado para banir: {userId}");
                return NotFound("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao banir usuário {userId}.");
                return StatusCode(500, "Erro interno ao banir usuário.");
            }
        }

        [HttpPost("UnbanUser/{userId}")]
        public async Task<IActionResult> UnbanUser(int userId)
        {
            try
            {
                await _adminService.UnbanUserAsync(userId);

                _logger.LogInformation($"Usuário {userId} desbanido.");
                return Ok($"Usuário {userId} desbanido.");
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Usuário não encontrado para desbanir: {userId}");
                return NotFound("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao desbanir usuário {userId}.");
                return StatusCode(500, "Erro interno ao desbanir usuário.");
            }
        }

        [HttpGet("ListUsers")]
        public async Task<IActionResult> ListUsers()
        {
            try
            {
                var users = await _adminService.GetAllUsersAsync();
                _logger.LogInformation($"Total de usuários listados: {users.Count}");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar usuários.");
                return StatusCode(500, "Erro interno ao listar usuários.");
            }
        }

        [HttpGet("ListBannedUsers")]
        public async Task<IActionResult> ListBannedUsers()
        {
            try
            {
                var users = await _adminService.GetAllBannedUsersAsync();
                _logger.LogInformation($"Total de usuários banidos: {users.Count}");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar usuários banidos.");
                return StatusCode(500, "Erro interno ao listar usuários banidos.");
            }
        }
    }
}
