namespace QuantumWorld.Core.Domain
{
    public class OutsidersEnemy : Enemy
    {
        public override string Description => "Outsiders Description";        

        protected override TimeSpan BaseTimeToAttack => TimeSpan.FromSeconds(60);

        protected override float TimeMultiplier => 1;

        protected override List<Resource> Rewards => new List<Resource>()
        {
            new CarbonFiberResource(15000),
            new QuantumGlassResource(15000),
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new LightFighterShip(50),
            new HeavyFighterShip(25),
        };
    }
}