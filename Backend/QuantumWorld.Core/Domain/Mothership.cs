namespace QuantumWorld.Core.Domain
{
    public class Mothership : Ship
    {
        public override string Description => "Mothership Description";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(200);

        protected override int BaseHealthPoints => 50000;
        protected override int BaseAttackPower => 50000;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(50000),
            new QuantumGlassResource(50000),
            new HiggsBosonResource(10000),
        };

        public Mothership() : base()
        {

        }
        public Mothership(int count) : base(count)
        {

        }
    }
}