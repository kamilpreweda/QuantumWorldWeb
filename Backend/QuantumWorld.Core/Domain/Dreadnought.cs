namespace QuantumWorld.Core.Domain
{
    public class Dreadnought : Ship
    {
        public override string BaseDescription => "The Dreadnought is an awe-inspiring war machine that is capable of transforming from a massive spacecraft into a towering walking behemoth that can be deployed on hostile planets from orbit. This ability to transition from space to planetary combat makes the Dreadnought one of the most versatile and powerful spacecraft in the galaxy. Standing over 100 meters tall and weighing over 100,000 tons, the Dreadnought is an imposing presence on any battlefield.";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override float BaseTimeToBuildInSeconds => 15;

        protected override int BaseHealthPoints => 500;
        protected override int BaseAttackPower => 250;
        protected override int BaseSpaceshipFactoryLevelRequirement => 10;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(8000),
            new QuantumGlassResource(5000),
            new HiggsBosonResource(2000)
        };

        public Dreadnought() : base()
        {

        }
        public Dreadnought(int count) : base(count)
        {

        }
    }
}