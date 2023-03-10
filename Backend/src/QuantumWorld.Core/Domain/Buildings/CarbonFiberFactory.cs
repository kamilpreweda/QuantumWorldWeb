namespace QuantumWorld.Core.Domain
{
    public class CarbonFiberFactory : Building
    {
        public override string BaseDescription => "Carbon Fiber has become the backbone of the construction industry. Its strength and durability make it the ideal material for constructing buildings and spaceships that can withstand the harshest of environments. With the ever-increasing demands for larger and more complex structures, Carbon Fiber has become an essential resource for society to thrive and progress. The Carbon Fiber Factory is where the raw material is refined, purified, and transformed into the building blocks of the future. Without this factory, progress and expansion would come to a grinding halt, leaving humanity stranded and unable to reach the stars.";

        protected override float CostMultiplier => 2;

        protected override float TimeMultiplier => 2;

        protected override float BaseTimeToBuildInSeconds => 1;

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(150),
            new QuantumGlassResource(50),
        };
    }
}