using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FCG.Domain.Interfaces.Repository
{
    public interface IAdminRepository
    {
        Task CreateGameAsync(Admin game);
        Task UpdateUserAsync(Admin user);
        Task<bool> VerifyIfExistsIdAsync(Admin user);
    }
}
