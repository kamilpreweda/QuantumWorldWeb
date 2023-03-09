namespace QuantumWorld.Core.Domain
{
    public class ArtOfWarResearch : Research
    {
        public override string BaseDescription => "The Art of War research is based on a deep understanding of the physics and mechanics of space combat. Researchers study the behavior of fleets of spaceships in different scenarios and develop mathematical models to simulate various combat situations. By analyzing these simulations and identifying key strategic advantages, commanders can develop plans to outsmart their opponents and gain the upper hand in battle. Art of War research is a critical breakthrough in humanity's ability to wage war on a galactic scale.";

        protected override float CostMultiplier => 4;

        protected override float TimeMultiplier => 1;

        protected override float BaseTimeToBuildInSeconds => 20;

        protected override int BaseLabolatoryLevelRequirement => 2;

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