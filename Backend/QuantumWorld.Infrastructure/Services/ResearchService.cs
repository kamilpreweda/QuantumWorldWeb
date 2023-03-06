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

        public async Task CheckConstructionDates(User user)
        {
            DateTime now = DateTime.UtcNow;
            foreach (var research in user.Research)
            {
                if (research.ConstructionStartDate != null)
                {
                    TimeSpan timeSpan = (TimeSpan)(now - research.ConstructionStartDate);
                    float timeSpanInSeconds = (float)Math.Round(timeSpan.TotalSeconds);
                    if (timeSpanInSeconds >= research.TimeToBuildInSeconds)
                    {
                        research.ClearConstructionStartDate();
                        user.UpgradeResearch(research.Type);
                        research.IsResearchUnderConstruction(false);
                    }
                    else if (timeSpanInSeconds < research.TimeToBuildInSeconds)
                    {
                        research.SetTimeToBuildInSeconds(research.TimeToBuildInSeconds - timeSpanInSeconds);
                        research.SetConstructionStartDate(DateTime.UtcNow);
                        research.IsResearchUnderConstruction(true);
                    }
                }
            }
            await Task.CompletedTask;
        }

        public async Task SetConstructionStartDate(ResearchType type, string username, DateTime date)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            var research = user.Research.Find(r => r.Type == type);
            research.SetConstructionStartDate(date);
            research.IsResearchUnderConstruction(true);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;
        }

        public async Task UpgradeResearch(ResearchType type, string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            var research = user.Research.Find(r => r.Type == type);
            user.UpgradeResearch(type);
            research.ClearConstructionStartDate();
            research.IsResearchUnderConstruction(false);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;

        }
    }
}