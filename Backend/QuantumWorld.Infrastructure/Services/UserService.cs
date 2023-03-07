using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtService;
        private readonly IResourceService _resourceService;
        private readonly IBuildingService _buildingService;
        private readonly IResearchService _researchService;

        private readonly IShipService _shipService;
        private readonly IBattleService _battleService;

        public UserService(IUserRepository userRepository, IMapper mapper, IEncrypter encrypter, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IJwtService jwtService, IResourceService resourceService, IBuildingService buildingService, IResearchService researchService, IShipService shipService, IBattleService battleService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
            _resourceService = resourceService;
            _buildingService = buildingService;
            _researchService = researchService;
            _shipService = shipService;
            _battleService = battleService;
        }

        public async Task<UserDto> GetAsync(string username)
        {
            var user = _userRepository.GetByUsername(username);
            List<Resource> resources = _resourceService.GetUserResources(user);
            user.Resources = resources;
            await _buildingService.CheckConstructionDates(user);
            await _researchService.CheckConstructionDates(user);
            await _shipService.CheckConstructionDates(user);
            await _battleService.CheckAttackDates(user);
            await _userRepository.UpdateAsync(user);
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

            JwtDto token = _jwtService.CreateToken(username);

            RefreshToken refreshToken = _jwtService.GenerateRefreshToken();
            _jwtService.SetRefreshToken(refreshToken, user);
            await _userRepository.UpdateAsync(user);
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

        public string GetMyId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return result;
        }

        public string GetRefreshToken(string username)
        {

            var user = _userRepository.GetByUsername(username);
            var refreshToken = user.RefreshToken;
            return refreshToken;
        }
    }
}