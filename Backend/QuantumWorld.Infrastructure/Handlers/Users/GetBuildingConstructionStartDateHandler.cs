using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class GetBuildingConstructionStartDateHandler : IRequestHandler<GetBuildingConstructionStartDate, Unit>
    {
        private readonly IBuildingService _buildingService;

        public GetBuildingConstructionStartDateHandler(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        public async Task<Unit> Handle(GetBuildingConstructionStartDate request, CancellationToken cancellationToken)
        {
            await _buildingService.SetConstructionStartDate(request.type, request.username, request.date);
            return Unit.Value;
        }
    }
}