using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IResearchService
    {
        Task UpgradeResearch(ResearchType type, string email);
    }
}