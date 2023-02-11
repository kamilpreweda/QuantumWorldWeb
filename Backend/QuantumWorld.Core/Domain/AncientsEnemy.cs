namespace QuantumWorld.Core.Domain
{
    public class AncientsEnemy : Enemy
    {
        public override string Description => "Ancients Enemy Description";

        protected override TimeSpan BaseTimeToAttack => TimeSpan.FromSeconds(200);

        protected override float TimeMultiplier => 1;

        public override List<Resource> BaseRewards => new List<Resource>()
        {
            new CarbonFiberResource(500000),
            new QuantumGlassResource(500000),
            new HiggsBosonResource(500000)
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new Destroyer(500),
            new Dreadnought(250),
            new Mothership(1)
        };
    }
}