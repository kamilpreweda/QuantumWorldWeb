namespace QuantumWorld.Core.Domain
{
    public class Mothership : Ship
    {
        public override string BaseDescription => "The Mothership is the pinnacle of spacecraft engineering, a marvel of advanced technology and engineering that is unmatched by any other vessel in the galaxy. Its size and power are truly overwhelming. Only the most advanced civilizations in the universe are capable of building such a machine, and the sheer cost and resources required to construct a Mothership make it a rare sight indeed. However, those who do possess these massive vessels wield incredible power and influence, able to project their might across the galaxy and assert their dominance over all who oppose them.";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override float BaseTimeToBuildInSeconds => 18;

        protected override int BaseHealthPoints => 50000;
        protected override int BaseAttackPower => 50000;
        protected override int BaseSpaceshipFactoryLevelRequirement => 12;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(50000),
            new QuantumGlassResource(50000),
            new HiggsBosonResource(10000),
        };

        public Mothership() : base()
        {

        }
        public Mothership(int count) : base(count)
        {

        }
    }
}