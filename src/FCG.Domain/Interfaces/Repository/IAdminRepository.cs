using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces.Repository
{
    public interface IAdminRepository
    {
        Task<bool> VerifyIfExistsIdAsync(Admin user);
    }
}
