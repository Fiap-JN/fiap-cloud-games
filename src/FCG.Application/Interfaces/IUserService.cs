using FCG.Application.Requests;
using FCG.Application.Responses;
using FCG.Domain.Entities;


namespace FCG.Application.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserResponses> CreateUserAsync(CreateUserRequest createUserRequest);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();


    }
}
