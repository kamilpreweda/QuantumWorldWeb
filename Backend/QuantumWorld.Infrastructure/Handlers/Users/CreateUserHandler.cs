using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUser, Unit>
    {
        private readonly IUserService _userService;


        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            await _userService.RegisterAsync(request.Email, request.Username, request.Password);
            return Unit.Value;
        }
    }
}
