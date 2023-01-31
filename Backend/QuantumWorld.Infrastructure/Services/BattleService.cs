using QuantumWorld.Core.Domain;
using QuantumWorld.Core.Repositories;

namespace QuantumWorld.Infrastructure.Services
{
    public class BattleService : IBattleService
    {
        private readonly IUserRepository _userRepository;
        public async Task StartBattle(EnemyType type, string email)
        {
            var user = _userRepository.Get(email);
            if (user is not null)
            {
                throw new Exception($"User with {email} email already exists!");
            }
            user.StartBattle(type);
            await Task.CompletedTask;
        }
    }
}