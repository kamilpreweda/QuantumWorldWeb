namespace QuantumWorld.Core.Domain
{
    public class Labolatory : Building
    {
        public override string Description => "Labolatory Description";

        protected override float CostMultiplier => 2;

        protected override float TimeMultiplier => 2;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(5);

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(350),
            new QuantumGlassResource(350),
        };
    }
}