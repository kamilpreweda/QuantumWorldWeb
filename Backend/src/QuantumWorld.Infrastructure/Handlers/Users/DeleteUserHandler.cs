using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser, Unit>
    {
        private readonly IUserService _userService;
        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            await _userService.DeleteAsync(request.Username, request.Password);
            return Unit.Value;
        }
    }
}
