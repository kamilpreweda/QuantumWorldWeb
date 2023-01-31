using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class StartBattleHandler : IRequestHandler<StartBattle, Unit>
    {
        private readonly IBattleService _battleService;

        public StartBattleHandler(IBattleService battleService)
        {
            _battleService = battleService;
        }
        public async Task<Unit> Handle(StartBattle request, CancellationToken cancellationToken)
        {
            _battleService.StartBattle(request.type, request.email);
            return Unit.Value;
        }
    }
}