namespace QuantumWorld.Core.Domain
{
    public class HeavyFighterShip : Ship
    {
        public override string Description => "Heavy Fighter Ship Description";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(6);

        protected override int BaseHealthPoints => 50;
        protected override int BaseAttackPower => 30;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(200),
            new QuantumGlassResource(100),
        };

        public HeavyFighterShip() : base()
        {

        }
        public HeavyFighterShip(int count) : base(count)
        {

        }
    }
}