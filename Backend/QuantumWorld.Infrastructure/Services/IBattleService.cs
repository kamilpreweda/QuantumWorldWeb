using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.Services
{
    public interface IBattleService
    {
        Task StartBattle(EnemyType type, string email);
    }
}