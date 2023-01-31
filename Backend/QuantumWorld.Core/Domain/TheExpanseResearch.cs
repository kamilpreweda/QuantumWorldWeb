namespace QuantumWorld.Core.Domain
{
    public class TheExpanseResearch : Research
    {
        public override string Description => "The Expanse Research Description";

        protected override float CostMultiplier => 3;

        protected override float TimeMultiplier => 3;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(10);

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(100),
            new QuantumGlassResource(100),
        };
    }
}