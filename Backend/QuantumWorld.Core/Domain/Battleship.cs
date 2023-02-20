namespace QuantumWorld.Core.Domain
{
    public class Battleship : Ship
    {
        public override string BaseDescription => "The Battleship is a massive, heavily-armed spacecraft designed to dominate the battlefield with its superior firepower and advanced technology. Its size and power make it a formidable opponent for any enemy fleet, and its advanced systems and weapons allow it to engage in a wide variety of combat scenarios. The Battleship is the ultimate expression of military might in the galaxy, a true behemoth of a spacecraft designed to bring overwhelming force to bear on any enemy.";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override TimeSpan BaseTimeToBuild => TimeSpan.FromSeconds(8);

        protected override int BaseHealthPoints => 100;
        protected override int BaseAttackPower => 75;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(300),
            new QuantumGlassResource(200),
            new HiggsBosonResource(500)
        };

        public Battleship() : base()
        {

        }
        public Battleship(int count) : base(count)
        {

        }
    }
}