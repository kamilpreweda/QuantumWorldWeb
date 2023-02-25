using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.DTO;
using QuantumWorld.Infrastructure.Extensions;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class GenerateRefreshTokenHandler : IRequestHandler<GenerateRefreshToken, Unit>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenerateRefreshTokenHandler(IUserService userService, IUserRepository userRepository, IJwtService jwtService, IMemoryCache cache, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userService = userService;
            _jwtService = jwtService;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(GenerateRefreshToken request, CancellationToken cancellationToken)
        {
            request.TokenId = Guid.NewGuid();
            var user = _userRepository.GetByUsername(request.Username);
            var jwt = _jwtService.CreateToken(request.Username);
            _cache.SetJwt(request.TokenId, jwt);
            var refreshToken = _jwtService.GenerateRefreshToken();
            _jwtService.SetRefreshToken(refreshToken, user);
            await _userRepository.UpdateAsync(user);
            return Unit.Value;
        }
    }
}