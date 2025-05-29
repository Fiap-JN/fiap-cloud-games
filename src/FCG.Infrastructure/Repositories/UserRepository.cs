using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;
using FCG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace FCG.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            bool exists = await _context.Users.AnyAsync(u => u.Id == user.Id);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
