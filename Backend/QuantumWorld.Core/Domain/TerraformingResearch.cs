namespace QuantumWorld.Core.Domain
{
    public class TerraformingResearch : Research
    {
        public override string Description => "Terraforming Research Description";

        protected override float CostMultiplier => 5;

        protected override float TimeMultiplier => 5;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(50);

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(1000),
            new QuantumGlassResource(1000),
        };
        public TerraformingResearch() : base()
        {

        }
        public TerraformingResearch(int level) : base(level)
        {

        }
    }
}