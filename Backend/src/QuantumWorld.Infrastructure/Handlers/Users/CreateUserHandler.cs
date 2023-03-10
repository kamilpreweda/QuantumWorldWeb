using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Exceptions;
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
            var existingUser = await _userService.GetAsync(request.Username);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.Username);
            }
            await _userService.RegisterAsync(request.Password, request.Username);
            return Unit.Value;
        }
    }
}
