namespace QuantumWorld.Core.Domain
{
    public class Labolatory : Building
    {
        public override string BaseDescription => "The pursuit of scientific knowledge remains an important aspect of society. The Laboratory is a sprawling facility where researchers from all over the galaxy come to work on groundbreaking research and breakthrough discoveries. Laboratory is a critical hub of scientific innovation. It is where some of the brightest minds from around the galaxy come together to work on breakthrough research that has the potential to change the course of human history. The laboratory is a testament to the enduring human desire to push the boundaries of knowledge and explore the mysteries of the universe.";

        protected override float CostMultiplier => 2;

        protected override float TimeMultiplier => 2;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(5);

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(350),
            new QuantumGlassResource(350),
        };
    }
}