using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IResearchService
    {
        Task UpgradeResearch(ResearchType type, string email);
        Task SetConstructionStartDate(ResearchType type, string username, DateTime date);
        Task CheckConstructionDates(User user);
    }
}