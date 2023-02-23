using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Services
{
    public class BattleService : IBattleService
    {
        private readonly IUserRepository _userRepository;
        public BattleService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task StartBattle(EnemyType type, string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            user.StartBattle(type);
            await _userRepository.UpdateAsync(user);
        }
    }
}