namespace QuantumWorld.Core.Domain
{
    public class ArtOfWarResearch : Research
    {
        public override string BaseDescription => "The Art of War research is based on a deep understanding of the physics and mechanics of space combat. Researchers study the behavior of fleets of spaceships in different scenarios and develop mathematical models to simulate various combat situations. By analyzing these simulations and identifying key strategic advantages, commanders can develop plans to outsmart their opponents and gain the upper hand in battle. Art of War research is a critical breakthrough in humanity's ability to wage war on a galactic scale.";

        protected override float CostMultiplier => 1.6F;

        protected override float TimeMultiplier => 2;

        protected override float BaseTimeToBuildInSeconds => 6;

        protected override int BaseLabolatoryLevelRequirement => 2;

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(2000),
            new QuantumGlassResource(2000),
            new HiggsBosonResource (1000)
        };
        public ArtOfWarResearch() : base()
        {

        }
        public ArtOfWarResearch(int level) : base(level)
        {

        }
        
    }
}