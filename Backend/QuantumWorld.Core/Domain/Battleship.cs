namespace QuantumWorld.Core.Domain
{
    public class Battleship : Ship
    {
        public override string Description => "Battleship Description";

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