namespace QuantumWorld.Core.Domain
{
    public class HiggsBosonDetector : Building
    {
        public override string BaseDescription => "Higgs Boson is the fundamental particle that gives mass to all matter in the universe. The Higgs Boson Detector is a crucial device that enables scientists to capture and study these elusive particles. By analyzing the behavior of Higgs Bosons, scientists are able to uncover new insights into the nature of the universe, leading to breakthroughs in propulsion technology. The drive systems of advanced spaceships rely on the manipulation of Higgs Bosons to create and manipulate gravity fields.";

        protected override float CostMultiplier => 4;

        protected override float TimeMultiplier => 4;

        protected override float BaseTimeToBuildInSeconds => 3;

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(250),
            new QuantumGlassResource(150),
        };
    }
}