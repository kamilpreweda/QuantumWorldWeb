namespace QuantumWorld.Core.Domain
{
    public class Enemy
    {
        public EnemyType Type { get; protected set; }
        public bool IsKilled { get; protected set; }
        public string Description { get; protected set; }
        public TimeSpan TimeToAttack { get; protected set; }
        public IEnumerable<Research> Requirements { get; protected set; }
        public IEnumerable<Resource> Rewards { get; protected set; }
        public IEnumerable<Ship> Ships { get; protected set; }
    }
}