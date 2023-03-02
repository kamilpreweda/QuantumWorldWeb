using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class GetConstructionStartDateHandler : IRequestHandler<GetConstructionStartDate, Unit>
    {
        private readonly IBuildingService _buildingService;

        public GetConstructionStartDateHandler(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        public async Task<Unit> Handle(GetConstructionStartDate request, CancellationToken cancellationToken)
        {
            await _buildingService.SetConstructionStartDate(request.type, request.username, request.date);
            return Unit.Value;
        }
    }
}