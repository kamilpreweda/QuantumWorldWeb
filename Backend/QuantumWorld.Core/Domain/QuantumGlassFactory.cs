namespace QuantumWorld.Core.Domain
{
    public class QuantumGlassFactory : Building
    {
        public override string Description => "Quantum Glass Factory Description";

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(3);
        protected override float TimeMultiplier => 3;

        protected override float CostMultiplier => 3;

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(150),
            new QuantumGlassResource(100)
        };
    }
}