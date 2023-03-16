namespace QuantumWorld.Core.Domain
{
    public class HyperdriveResearch : Research
    {
        public override string BaseDescription => "The Hyperdrive research is based on a revolutionary new propulsion system that harnesses the power of quantum entanglement to achieve faster-than-light travel. This technology allows spaceships to create stable wormholes in space-time, bypassing the traditional limitations of distance and speed. With Hyperdrive, a journey that once took decades can now be completed in a matter of days or even hours.";

        protected override float CostMultiplier => 1.7F;

        protected override float TimeMultiplier => 2;

        protected override float BaseTimeToBuildInSeconds => 8;

        protected override int BaseLabolatoryLevelRequirement => 3;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(3000),
            new QuantumGlassResource(3000),
            new HiggsBosonResource(2000)
        };
        public HyperdriveResearch() : base()
        {

        }
        public HyperdriveResearch(int level) : base(level)
        {

        }
    }
}