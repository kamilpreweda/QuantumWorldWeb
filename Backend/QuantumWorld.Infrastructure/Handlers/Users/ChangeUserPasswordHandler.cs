using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPassword, Unit>
    {
        public async Task<Unit> Handle(ChangeUserPassword request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return Unit.Value;
        }
    }
}