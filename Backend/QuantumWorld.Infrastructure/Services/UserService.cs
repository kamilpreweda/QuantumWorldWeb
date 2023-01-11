using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto GetUser(string email)
        {
            var user = _userRepository.Get(email);
            return new UserDto
            {
                Username = user.Username,
                Email = user.Email
            };
        }

        public void Register(string email, string password, string username)
        {
            var user = _userRepository.Get(email);
            if (user is not null)
            {
                throw new Exception($"User with {email} email already exists!");
            }
            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, password, salt, username);
            _userRepository.Add(user);
        }
    }
}