namespace QuantumWorld.Core.Domain
{
    public class Destroyer : Ship
    {
        public override string BaseDescription => "The Destroyer is the ultimate expression of military power in the galaxy, a true juggernaut of a spacecraft that can turn the tide of any battle. Whether engaging in large-scale fleet battles or surgical strikes on key enemy targets, the Destroyer is a force to be reckoned with, capable of overwhelming even the most advanced enemy defenses.";

        protected override float CostMultiplier => 1;

        protected override float TimeMultiplier => 1;

        protected override float BaseTimeToBuildInSeconds => 12;

        protected override int BaseHealthPoints => 200;
        protected override int BaseAttackPower => 300;
        protected override int BaseSpaceshipFactoryLevelRequirement => 4;
        protected override List<Resource> BaseCost => new List<Resource>()
        {
            new CarbonFiberResource(5000),
            new QuantumGlassResource(4000),
            new HiggsBosonResource(2000)
        };

        public Destroyer() : base()
        {

        }
        public Destroyer(int count) : base(count)
        {

        }
    }
}