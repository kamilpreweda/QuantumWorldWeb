using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, IEncrypter encrypter, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
            _configuration = configuration;
        }

        public async Task<UserDto> GetAsync(string username)
        {
            var user = _userRepository.GetByUsername(username);
            await Task.CompletedTask;
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.BrowseAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(string password, string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is not null)
            {
                throw new Exception($"User with {username} username already exists!");
            }
            _encrypter.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user = new User(new Guid(), passwordSalt, passwordHash, username);
            await _userRepository.AddAsync(user);
            await Task.CompletedTask;
        }

        public async Task LoginAsync(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user == null)
            {
                throw new Exception($"Invalid credentials");
            }
            else if (!_encrypter.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception($"Invalid credentials");
            }

            string token = CreateToken(user);
            await Task.CompletedTask;
            return;

        }

        public async Task DeleteAsync(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

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

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}