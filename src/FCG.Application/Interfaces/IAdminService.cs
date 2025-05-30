using FCG.Domain.Entities;

namespace FCG.Application.Interfaces
{
    public interface IAdminService
    {
        Task PromoteUserAsync(int userId);
        Task BanUserAsync(int userId);
        Task UnbanUserAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllBannedUsersAsync();
    }
}
