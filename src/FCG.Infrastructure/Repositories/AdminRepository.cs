using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;
using FCG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateGameAsync(Admin game)
        {
            await _context.Game.AddAsync(game);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateUserAsync(Admin user)
        {
            bool exists = await _context.Users.AnyAsync(u => u.Id == user.Id);
            await _context.UserUpdateForAdmin.AddAsync(user);
        }

        public async Task<bool> VerifyIfExistsIdAsync(Admin user)
        {
            bool exists = await _context.Users.AnyAsync(u => u.Id == user.Id);
            return exists;
        }

    }
}
