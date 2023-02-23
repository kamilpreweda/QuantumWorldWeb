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
        public async Task UpgradeResearch(ResearchType type, string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            user.UpgradeResearch(type);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;

        }
    }
}