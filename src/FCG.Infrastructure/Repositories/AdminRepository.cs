using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;
using FCG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<bool> VerifyIfExistsIdAsync(Admin user)
        {
            bool exists = await _context.Users.AnyAsync(u => u.Id == user.Id);
            return exists;
        }

    }
}
