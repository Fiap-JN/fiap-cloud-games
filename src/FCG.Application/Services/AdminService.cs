using FCG.Application.Interfaces;
using FCG.Application.Requests;
using FCG.Application.Responses;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<CreateGameResponses> CreateGameAsync(CreateGameRequest createGameRequest)
        {
            var game = Admin.Create(createGameRequest.Name, createGameRequest.Price, createGameRequest.Gender);

            await _adminRepository.CreateGameAsync(game);   

            return new CreateGameResponses
            {
                Id = game.Id,
                Name = game.Name,
                Price = game.Price,
                Gender = game.Gender,
                CreatedAt = game.CreationDate
            };
        }

        public async Task<UpdateUserResponses> UpdateUserAsync(UpdateUserRequest updateUserRequest)
        {
            var user = Admin.UpdateUser(updateUserRequest.Id);

            await _adminRepository.UpdateUserAsync(user);

            Task<bool> exists = _adminRepository.VerifyIfExistsIdAsync(user);

            bool isAdmin = user.IsAdmin;

            if(exists.Result)
                user.IsAdmin = true;

            return new UpdateUserResponses
            {
                Id = user.Id,
                Exists = exists.Result
            };
        }
    }
}
