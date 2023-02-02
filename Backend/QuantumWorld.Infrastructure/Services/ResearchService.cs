using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Services
{
    public class ResearchService : IResearchService
    {
        private readonly IUserRepository _userRepository;
        public ResearchService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task UpgradeResearch(ResearchType type, string email)
        {
            var user = _userRepository.Get(email);
            if (user is null)
            {
                throw new Exception($"User with {email} doesn't exist!");
            }
            user.UpgradeResearch(type);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;

        }
    }
}