namespace QuantumWorld.Core.Domain
{
    public class AncientsEnemy : Enemy
    {
        public override string BaseDescription => "The Ancients are the stuff of legends, whispered about in hushed tones by those who have glimpsed the edges of the universe. It is said that they possess unimaginable power, able to manipulate the fabric of space and time itself. But they are also shrouded in mystery, and no one truly knows where they come from or what their intentions are. Some believe they are benevolent beings, watching over the universe from a distance. Others fear they are malevolent and will stop at nothing to exert their will over all creation. Whatever the truth may be, one thing is certain: to face the Ancients in battle would require the most advanced technology and tactics humanity has ever seen.";

        protected override float BaseTimeToAttackInSeconds => 200;

        protected override float TimeMultiplier => 1;

        public override List<Resource> BaseRewards => new List<Resource>()
        {
            new CarbonFiberResource(500000),
            new QuantumGlassResource(500000),
            new HiggsBosonResource(500000)
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new Destroyer(500),
            new Dreadnought(250),
            new Mothership(1)
        };
        public override List<Research> BaseRequirements => new List<Research>()
        {
            new TheExpanseResearch(14),
            new ArtOfWarResearch(12),
            new HyperdriveResearch(10),
        };
    }
}