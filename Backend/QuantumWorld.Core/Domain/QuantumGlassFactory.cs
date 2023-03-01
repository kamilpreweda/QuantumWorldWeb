namespace QuantumWorld.Core.Domain
{
    public class QuantumGlassFactory : Building
    {
        public override string BaseDescription => "Quantum Mechanics has advanced to the point where it has revolutionized the construction industry. Quantum Glass is a breakthrough material that is synthesized using quantum mechanics principles. It is an ultra-strong and ultra-transparent material that is used for building structures that can withstand the most extreme conditions. With the ability to harness the power of quantum mechanics, the Quantum Glass Factory is at the forefront of modern technology, paving the way for a brighter and more advanced future.";

        protected override float BaseTimeToBuildInSeconds => 2;
        protected override float TimeMultiplier => 3;

        protected override float CostMultiplier => 3;

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(150),
            new QuantumGlassResource(100)
        };
    }
}