using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Services
{
    public class ResearchService : IResearchService
    {
        private readonly IUserRepository _userRepository;
        public async Task UpgradeResearch(ResearchType type, string email)
        {
            var user = _userRepository.Get(email);
            if (user is not null)
            {
                throw new Exception($"User with {email} email already exists!");
            }
            user.UpgradeResearch(type);
            await Task.CompletedTask;

        }
    }
}