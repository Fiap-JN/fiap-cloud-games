using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;
using FCG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FCG.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Usuário criado: Id={user.Id}, Email={user.Email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao criar usuário: Email={user.Email}");
                throw;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    _logger.LogInformation($"Usuário encontrado: Id={user.Id}, Email={user.Email}");
                }
                else
                {
                    _logger.LogWarning($"Usuário não encontrado: Email={email}");
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar usuário por email: {email}");
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _logger.LogInformation($"Usuário encontrado: Id={user.Id}");
                }
                else
                {
                    _logger.LogWarning($"Usuário não encontrado: Id={id}");
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar usuário por Id={id}");
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                _logger.LogInformation($"Total de usuários retornados: {users.Count}");
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os usuários.");
                throw;
            }
        }

        public async Task<List<User>> GetAllBannedUsersAsync()
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.IsBanned)
                    .ToListAsync();

                _logger.LogInformation($"Total de usuários banidos retornados: {users.Count}");
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuários banidos.");
                throw;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Usuário atualizado: Id={user.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar usuário: Id={user.Id}");
                throw;
            }
        }
    }
}
