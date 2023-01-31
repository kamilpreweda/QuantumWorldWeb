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

        public async Task RegisterAsync(Guid userId, string email, string password, string username, List<Resource> resources, List<Building> buildings, List<Research> research, List<Ship> ships, List<Enemy> enemies, IBattle battle)
        {
            var user = _userRepository.Get(email);
            if (user is not null)
            {
                throw new Exception($"User with {email} email already exists!");
            }
            _encrypter.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user = new User(userId, email, password, passwordSalt, passwordHash, username, resources, buildings, research, ships, enemies, battle);
            _userRepository.Add(user);
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

            return;
        }

        public async Task SetBuilding(Guid userId, BuildingType type, int level, string description, TimeSpan timeToBuild, float CostMultiplier, IEnumerable<Resource> cost)
        {
            //     var user = _userRepository.Get(userId);
            //     user.SetBuilding();
            //     _userRepository.Update(user);
            // }
        }
    }
}