namespace QuantumWorld.Core.Domain
{
    public class Destroyer : Ship
    {
        public override string Description => "Destroyer Description";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(10);

        protected override int BaseHealthPoints => 200;
        protected override int BaseAttackPower => 300;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(5000),
            new QuantumGlassResource(4000),
            new HiggsBosonResource(1500)
        };

        public Destroyer() : base()
        {

        }
        public Destroyer(int count) : base(count)
        {

        }
    }
}