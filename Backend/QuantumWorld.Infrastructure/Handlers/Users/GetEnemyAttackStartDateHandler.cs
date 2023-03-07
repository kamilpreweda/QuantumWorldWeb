using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class GetEnemyAttackStartDateHandler : IRequestHandler<GetEnemyAttackStartDate, Unit>
    {
        private readonly IBattleService _battleService;

        public GetEnemyAttackStartDateHandler(IBattleService battleService)
        {
            _battleService = battleService;
        }

        public async Task<Unit> Handle(GetEnemyAttackStartDate request, CancellationToken cancellationToken)
        {
            await _battleService.SetAttackStartDate(request.type, request.username, request.date);
            return Unit.Value;
        }
    }
}