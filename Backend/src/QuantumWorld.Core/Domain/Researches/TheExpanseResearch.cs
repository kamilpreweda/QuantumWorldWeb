namespace QuantumWorld.Core.Domain
{
    public class TheExpanseResearch : Research
    {
        public override string BaseDescription => "The Expanse research is a groundbreaking discovery that has revolutionized humanity's ability to explore the vast reaches of space. With this technology, the farthest corners of the galaxy are now within reach, and distant planets that were once thought to be unreachable are now accessible. The Expanse research has unlocked the secrets of faster-than-light travel, allowing spaceships to traverse incredible distances in a fraction of the time it once took. The technology is based on an entirely new understanding of space-time, and relies on a complex network of quantum entanglements and wormholes to create a stable and navigable path through the void.";

        protected override float CostMultiplier => 1.1F;

        protected override float TimeMultiplier => 2;

        protected override float BaseTimeToBuildInSeconds => 4;

        protected override int BaseLabolatoryLevelRequirement => 1;

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(100),
            new QuantumGlassResource(100),
        };

        public TheExpanseResearch() : base()
        {

        }
        public TheExpanseResearch(int level) : base(level)
        {

        }
    }
}