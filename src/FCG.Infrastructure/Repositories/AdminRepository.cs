using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;
using FCG.Infrastructure.Data;
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
    }
}
