namespace QuantumWorld.Core.Domain
{
    public class SpaceshipFactory : Building
    {
        public override string BaseDescription => "The Spaceship Factory is a massive industrial complex that plays a critical role in the ongoing colonization effort. It is where the most advanced and powerful spaceships are created. The ships themselves are immense, with some spanning kilometers in length, and are designed to support entire communities of people and resources. The factory is a testament to humanity's ingenuity and determination to push the boundaries of what is possible, and its products are a symbol of our never-ending quest for exploration and discovery. Each Spaceship Factory level reduces time needed to build ships by half.";

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