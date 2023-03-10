using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class GetShipConstructionStartDateAndShipCountHandler : IRequestHandler<GetShipConstructionStartDateAndShipCount, Unit>
    {
        private readonly IShipService _shipService;

        public GetShipConstructionStartDateAndShipCountHandler(IShipService shipService)
        {
            _shipService = shipService;
        }
        public async Task<Unit> Handle(GetShipConstructionStartDateAndShipCount request, CancellationToken cancellationToken)
        {
            await _shipService.SetConstructionStartDateAndShipCount(request.type, request.username, request.date, request.count);
            return Unit.Value;
        }
    }
}