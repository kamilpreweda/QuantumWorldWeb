using MediatR;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Infrastructure.Handlers.Users
{
    public class UpgradeResearchHandler : IRequestHandler<UpgradeResearch, Unit>
    {
        private readonly IResearchService _researchService;

        public UpgradeResearchHandler(IResearchService researchService)
        {
            _researchService = researchService;
        }
        public async Task<Unit> Handle(UpgradeResearch request, CancellationToken cancellationToken)
        {
            await _researchService.UpgradeResearch(request.type, request.username);
            return Unit.Value;
        }
    }
}