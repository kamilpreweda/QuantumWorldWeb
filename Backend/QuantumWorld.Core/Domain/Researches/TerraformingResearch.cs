namespace QuantumWorld.Core.Domain
{
    public class TerraformingResearch : Research
    {
        public override string BaseDescription => "The Terraforming research is based on a deep understanding of the geological, atmospheric, and biological processes that shape a planet's environment. By carefully manipulating these processes, researchers can alter a planet's climate, composition, and ecosystem, making it more suitable for human life. The Terraforming research is a complex and multi-disciplinary field, drawing on expertise in engineering, geology, biology, and many other fields. It requires the use of cutting-edge technologies. Each Terraforming level increases availible space on planet by 15.";

        protected override float CostMultiplier => 5;

        protected override float TimeMultiplier => 1;

        protected override float BaseTimeToBuildInSeconds => 40;

        protected override int BaseLabolatoryLevelRequirement => 4;
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