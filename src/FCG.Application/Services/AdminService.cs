using FCG.Application.Interfaces;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;

namespace FCG.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;

        public AdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task PromoteUserAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    throw new KeyNotFoundException("Usuário não encontrado.");
                }

                user.IsAdmin = true;
                user.UpdateDate = DateTime.Now;

                await _userRepository.UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task BanUserAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    throw new KeyNotFoundException("Usuário não encontrado.");
                }

                user.IsBanned = true;
                user.UpdateDate = DateTime.UtcNow;

                await _userRepository.UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UnbanUserAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    throw new KeyNotFoundException("Usuário não encontrado.");
                }

                user.IsBanned = false;
                user.UpdateDate = DateTime.Now;

                await _userRepository.UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<User>> GetAllBannedUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllBannedUsersAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
