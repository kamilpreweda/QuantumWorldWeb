using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class LoginHandler : IRequestHandler<Login, Unit>
    {
        private readonly IUserService _userService;
        public LoginHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Unit> Handle(Login request, CancellationToken cancellationToken)
        {
            await _userService.LoginAsync(request.Username, request.Password);
            var user = await _userService.GetAsync(request.Username);
            return Unit.Value;
        }
    }
}