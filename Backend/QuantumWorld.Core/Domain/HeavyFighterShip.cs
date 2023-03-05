namespace QuantumWorld.Core.Domain
{
    public class HeavyFighterShip : Ship
    {
        public override string BaseDescription => "The Heavy Fighter is a larger, more powerful version of the Light Fighter, designed to pack a greater punch while sacrificing some of its agility. While not as nimble as its smaller counterpart, the Heavy Fighter makes up for it with its superior firepower and durability.";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override int BaseTimeToBuildInSeconds => 6;

        protected override int BaseHealthPoints => 50;
        protected override int BaseAttackPower => 30;
        protected override int BaseSpaceshipFactoryLevelRequirement => 4;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(200),
            new QuantumGlassResource(100),
        };

        public HeavyFighterShip() : base()
        {

        }
        public HeavyFighterShip(int count) : base(count)
        {

        }
    }
}