namespace QuantumWorld.Core.Domain
{
    public class RebelsEnemy : Enemy
    {
        public override string Description => "Rebels Enemy Description";

        protected override TimeSpan BaseTimeToAttack => TimeSpan.FromSeconds(80);

        protected override float TimeMultiplier => 1;

        protected override List<Resource> Rewards => new List<Resource>()
        {
            new CarbonFiberResource(25000),
            new QuantumGlassResource(25000),
            new HiggsBosonResource(10000)
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new Battleship(50),
            new Destroyer(50)
        };
    }
}