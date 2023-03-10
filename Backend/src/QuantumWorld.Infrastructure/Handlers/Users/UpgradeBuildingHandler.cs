using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class UpgradeBuildingHandler : IRequestHandler<UpgradeBuilding, Unit>
    {
        private readonly IBuildingService _buildingService;

        public UpgradeBuildingHandler(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }
        public async Task<Unit> Handle(UpgradeBuilding request, CancellationToken cancellationToken)
        {
            await _buildingService.UpgradeBuilding(request.type, request.username);
            return Unit.Value;
        }
    }
}