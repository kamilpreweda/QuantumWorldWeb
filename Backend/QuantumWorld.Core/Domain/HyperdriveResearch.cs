namespace QuantumWorld.Core.Domain
{
    public class HyperdriveResearch : Research
    {
        public override string Description => "Hyperdrive Research Description";

        protected override float CostMultiplier => 4;

        protected override float TimeMultiplier => 4;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(30);

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(300),
            new QuantumGlassResource(300),
            new HiggsBosonResource(200)
        };
    }
}