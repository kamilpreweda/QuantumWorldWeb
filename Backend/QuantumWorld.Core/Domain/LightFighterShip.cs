namespace QuantumWorld.Core.Domain
{
    public class LightFighterShip : Ship
    {
        public override string BaseDescription => "The Light Fighter is a small but highly agile spaceship designed for quick strikes and nimble maneuvers. While not the most powerful ship in the fleet, its speed and agility make it an ideal choice for reconnaissance missions, hit-and-run attacks, and surgical strikes on enemy targets. ";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(3);

        protected override int BaseHealthPoints => 20;
        protected override int BaseAttackPower => 10;

        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(100),
            new QuantumGlassResource(50),
        };

        public LightFighterShip() : base()
        {

        }
        public LightFighterShip(int count) : base(count)
        {

        }
    }
}