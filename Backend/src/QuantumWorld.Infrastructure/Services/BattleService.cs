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

        public async Task CheckAttackDates(User user)
        {
            DateTime now = DateTime.UtcNow;
            foreach (var enemy in user.Enemies)
            {
                if (enemy.AttackStartDate != null)
                {
                    TimeSpan timeSpan = (TimeSpan)(now - enemy.AttackStartDate);
                    float timeSpanInSeconds = (float)timeSpan.TotalSeconds;
                    if (timeSpanInSeconds >= enemy.TimeToAttackInSeconds)
                    {
                        enemy.ClearAttackStartDate();
                        user.StartBattle(enemy.Type);
                        enemy.IsEnemyUnderAttack(false);
                    }
                    else if (timeSpanInSeconds < enemy.TimeToAttackInSeconds)
                    {
                        enemy.SetTimeToAttackInSeconds(enemy.TimeToAttackInSeconds - timeSpanInSeconds);
                        enemy.SetAttackStartDate(DateTime.UtcNow);
                        enemy.IsEnemyUnderAttack(true);
                    }
                }
            }
            await Task.CompletedTask;
        }

        public async Task SetAttackStartDate(EnemyType type, string username, DateTime date)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null)
            {
                throw new Exception($"User with {username} name doesn't exist!");
            }
            var enemy = user.Enemies.Find(e => e.Type == type);
            enemy.SetAttackStartDate(date);
            enemy.IsEnemyUnderAttack(true);
            _userRepository.UpdateAsync(user);
            await Task.CompletedTask;
        }

    }
}