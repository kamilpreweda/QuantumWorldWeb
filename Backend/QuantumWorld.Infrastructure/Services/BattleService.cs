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
        public async Task StartBattle(EnemyType type, string email)
        {
            var user = _userRepository.Get(email);
            if (user is null)
            {
                throw new Exception($"User with {email} doesn't exist!");
            }
            user.StartBattle(type);
            await _userRepository.UpdateAsync(user);
        }
    }
}