using FCG.Application.Interfaces;
using FCG.Application.Requests;
using FCG.Application.Responses;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;


namespace FCG.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateUserResponses> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var user = User.Create(createUserRequest.Name, createUserRequest.Email, createUserRequest.Password);
            var existingUser = await _userRepository.GetUserByEmailAsync(createUserRequest.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("E-mail já cadastrado!");
            }

            await _userRepository.CreateUserAsync(user);

            return new CreateUserResponses
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreationDate
            };
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

    }
}
