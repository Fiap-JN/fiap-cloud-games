using FCG.Application.Interfaces;
using FCG.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using FCG.Domain.Entities;
using Microsoft.AspNetCore.Identity.Data;

namespace FCG.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IConfiguration configuration, ILogger<UserController> logger)
        {
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserRequest createUserRequest)
        {
            var user = await _userService.CreateUserAsync(createUserRequest);
            _logger.LogInformation($"Novo usuário criado com sucesso: {user.Name}");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(request.Email);

                if (user == null || user.Password != request.Password)
                {
                    _logger.LogWarning($"Tentativa de login com credenciais inválidas. Email: {request.Email}");
                    return Unauthorized("Credenciais inválidas.");
                }

                var role = user.IsAdmin ? "Admin" : "User";
                var token = GenerateJwtToken(user, role);

                _logger.LogInformation($"Login bem-sucedido para usuário: {user.Email}");
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer login.");
                return StatusCode(500, "Erro interno ao fazer login. Tente novamente.");
            }
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                _logger.LogInformation("Lista de usuários retornada com sucesso.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuários.");
                return StatusCode(500, "Erro interno ao buscar usuários. Tente novamente.");
            }
        }

        private string GenerateJwtToken(User user, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
