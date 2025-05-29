using FCG.Application.Requests;
using FCG.Application.Responses;
using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Interfaces
{
    public interface IAdminService
    {
        Task<CreateGameResponses> CreateGameAsync(CreateGameRequest createGameRequest);
        Task<UpdateUserResponses> UpdateUserAsync(UpdateUserRequest updateUserRequest);
    }
}
