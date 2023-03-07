using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IBattleService
    {
        Task StartBattle(EnemyType type, string email);
        Task SetAttackStartDate(EnemyType type, string username, DateTime date);
        Task CheckAttackDates(User user);
    }
}