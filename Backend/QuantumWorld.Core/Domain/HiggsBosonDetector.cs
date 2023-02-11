namespace QuantumWorld.Core.Domain
{
    public class HiggsBosonDetector : Building
    {
        public override string Description => "Higgs Boson Detector Description";

        protected override float CostMultiplier => 4;

        protected override float TimeMultiplier => 4;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(3);

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(250),
            new QuantumGlassResource(150),
        };
    }
}