using AutoMapper;
using MongoDB.Bson;
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

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.BrowseAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(Guid id, string email, string password, string username)
        {
            var user = _userRepository.Get(email);
            if (user is not null)
            {
                throw new Exception($"User with {email} email already exists!");
            }
            _encrypter.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user = new User(id, email, password, passwordSalt, passwordHash, username);
            await _userRepository.AddAsync(user);
            await Task.CompletedTask;
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = _userRepository.Get(email);

            if (user == null)
            {
                throw new Exception($"Invalid credentials");
            }
            else if (!_encrypter.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception($"Invalid credentials");
            }
            await Task.CompletedTask;
            return;

        }

        public async Task DeleteAsync(string email, string password)
        {
            var user = _userRepository.Get(email);

            if (user == null)
            {
                throw new Exception($"Invalid credentials");
            }
            else if (!_encrypter.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception($"Invalid credentials");
            }

            await _userRepository.RemoveAsync(user.Id);
        }
    }
}