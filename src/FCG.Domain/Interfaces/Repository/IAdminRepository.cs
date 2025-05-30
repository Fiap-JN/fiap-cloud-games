using FCG.Domain.Entities;


namespace FCG.Domain.Interfaces.Repository
{
    public interface IAdminRepository
    {
        Task UpdateUserAsync(Admin user);
        Task<bool> VerifyIfExistsIdAsync(Admin user);
    }
}
