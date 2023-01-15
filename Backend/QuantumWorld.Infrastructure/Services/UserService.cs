using AutoMapper;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto GetUser(string email)
        {
            var user = _userRepository.Get(email);
            return _mapper.Map<User, UserDto>(user);
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