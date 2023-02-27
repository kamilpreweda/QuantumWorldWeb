using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class BuildShipHandler : IRequestHandler<BuildShip, Unit>
    {
        private readonly IShipService _shipService;

        public BuildShipHandler(IShipService shipService)
        {
            _shipService = shipService;
        }

        public async Task<Unit> Handle(BuildShip request, CancellationToken cancellationToken)
        {
            await _shipService.BuildShip(request.type, request.count, request.username);
            return Unit.Value;
        }
    }
}