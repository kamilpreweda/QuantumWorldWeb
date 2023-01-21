namespace QuantumWorld.Core.Domain
{
    public class CarbonFiberFactory : Building
    {
        public override string Description => "Carbon Fiber Factory Description";

        protected override float CostMultiplier => 2;

        protected override float TimeMultiplier => 2;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(2);

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(150),
            new QuantumGlassResource(50),
        };
    }
}