namespace QuantumWorld.Core.Domain
{
    public class SpaceshipFactory : Building
    {
        public override string Description => "Spaceship Factory Description";

        protected override float CostMultiplier => 2;

        protected override float TimeMultiplier => 2;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(6);

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(450),
            new QuantumGlassResource(450),
        };
    }
}