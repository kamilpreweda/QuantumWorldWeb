namespace QuantumWorld.Core.Domain
{
    public class ArtOfWarResearch : Research
    {
        public override string Description => "The Expanse Research Description";

        protected override float CostMultiplier => 4;

        protected override float TimeMultiplier => 4;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(20);

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(200),
            new QuantumGlassResource(200),
        };
        public ArtOfWarResearch() : base()
        {

        }
        public ArtOfWarResearch(int level) : base(level)
        {

        }
        
    }
}