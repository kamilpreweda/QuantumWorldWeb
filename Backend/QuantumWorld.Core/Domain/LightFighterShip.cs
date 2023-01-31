namespace QuantumWorld.Core.Domain
{
    public class LightFighterShip : Ship
    {
        public override string Description => "Light Fighter Ship Description";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(3);

        protected override int BaseHealthPoints => 20;
        protected override int BaseAttackPower => 10;

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(100),
            new QuantumGlassResource(50),
        };

        public LightFighterShip() : base()
        {

        }
        public LightFighterShip(int count) : base(count)
        {

        }
    }
}