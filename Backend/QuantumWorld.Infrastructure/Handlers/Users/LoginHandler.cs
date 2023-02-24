using MediatR;
using Microsoft.Extensions.Caching.Memory;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Extensions;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class LoginHandler : IRequestHandler<Login, Unit>
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMemoryCache _cache;

        public LoginHandler(IUserService userService, IJwtService jwtService, IMemoryCache cache)
        {
            _userService = userService;
            _jwtService = jwtService;
            _cache = cache;
        }
        public async Task<Unit> Handle(Login request, CancellationToken cancellationToken)
        {
            await _userService.LoginAsync(request.Username, request.Password);
            var user = await _userService.GetAsync(request.Username);
            var jwt = _jwtService.CreateToken(request.Username);
            _cache.SetJwt(request.TokenId, jwt);
            return Unit.Value;
        }
    }
}