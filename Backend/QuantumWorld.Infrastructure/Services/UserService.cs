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
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IMapper mapper, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = _userRepository.Get(email);
            await Task.CompletedTask;
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task RegisterAsync(string email, string password, string username)
        {
            var user = _userRepository.Get(email);
            if (user is not null)
            {
                throw new Exception($"User with {email} email already exists!");
            }
            _encrypter.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user = new User(email, password, passwordSalt, passwordHash, username);
            _userRepository.Add(user);
            await Task.CompletedTask;
        }
    }
}