using FCG.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCG.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllBannedUsersAsync();
        Task UpdateUserAsync(User user);
    }
}
