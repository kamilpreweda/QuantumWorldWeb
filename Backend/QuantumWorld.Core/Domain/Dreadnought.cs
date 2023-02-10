namespace QuantumWorld.Core.Domain
{
    public class Dreadnought : Ship
    {
        public override string Description => "Dreadnought Description";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(12);

        protected override int BaseHealthPoints => 500;
        protected override int BaseAttackPower => 250;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(8000),
            new QuantumGlassResource(5000),
            new HiggsBosonResource(2000)
        };

        public Dreadnought() : base()
        {

        }
        public Dreadnought(int count) : base(count)
        {

        }
    }
}