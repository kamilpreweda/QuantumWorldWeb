namespace QuantumWorld.Core.Domain
{
    public class PiratesEnemy : Enemy
    {
        public override string Description => "Pirates Description";        

        protected override TimeSpan BaseTimeToAttack => TimeSpan.FromSeconds(30);

        protected override float TimeMultiplier => 1;

        public override List<Resource> BaseRewards => new List<Resource>()
        {
            new CarbonFiberResource(5000),
            new QuantumGlassResource(5000),
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new LightFighterShip(10),
            new HeavyFighterShip(10),
        };
    }
}