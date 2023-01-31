using Microsoft.Extensions.Logging;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {

        private readonly IUserService _userService;
        private readonly ILogger<IDataInitializer> _logger;
        private readonly IBattle _battle;

        public DataInitializer(IUserService userService, ILogger<DataInitializer> logger, IBattle battle)
        {
            _userService = userService;
            _logger = logger;
            _battle = battle;

        }
        public async Task SeedAsync()
        {
            _logger.LogTrace("Initializing data...");
            var tasks = new List<Task>();
            for (var i = 1; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";
                var resources = new List<Resource>();
                var buildings = new List<Building>();
                var research = new List<Research>();
                var ships = new List<Ship>();
                var enemies = new List<Enemy>();
                _logger.LogTrace($"Created a new user: '{username}'.");
                tasks.Add(_userService.RegisterAsync(userId, $"{username}@test.com", "secret", username));
            }
            await Task.WhenAll(tasks);
            _logger.LogTrace("Data was initialized.");
        }
    }
}