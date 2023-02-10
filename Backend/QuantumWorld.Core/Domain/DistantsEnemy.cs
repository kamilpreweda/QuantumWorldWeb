namespace QuantumWorld.Core.Domain
{
    public class DistantsEnemy : Enemy
    {
        public override string Description => "Distants Enemy Description";

        protected override TimeSpan BaseTimeToAttack => TimeSpan.FromSeconds(120);

        protected override float TimeMultiplier => 1;

        protected override List<Resource> Rewards => new List<Resource>()
        {
            new CarbonFiberResource(100000),
            new QuantumGlassResource(100000),
            new HiggsBosonResource(100000)
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new Battleship(200),
            new Destroyer(150),
            new Dreadnought(100)
        };
    }
}