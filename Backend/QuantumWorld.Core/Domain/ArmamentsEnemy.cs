namespace QuantumWorld.Core.Domain
{
    public class ArmamentsEnemy : Enemy
    {
        public override string Description => "Armaments Enemy Description";

        protected override TimeSpan BaseTimeToAttack => TimeSpan.FromSeconds(100);

        protected override float TimeMultiplier => 1;

        protected override List<Resource> Rewards => new List<Resource>()
        {
            new CarbonFiberResource(50000),
            new QuantumGlassResource(50000),
            new HiggsBosonResource(50000)
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new Destroyer(100),
            new Dreadnought(50)
        };
    }
}