namespace QuantumWorld.Core.Domain
{
    public class OutsidersEnemy : Enemy
    {
        public override string BaseDescription => "The Outsiders separated themselves from humanity and the rest of the galaxy. Preferring to live in isolation, they have little interest in interacting with other civilizations and are notoriously difficult to approach or communicate with. Despite their desire for solitude, the Outsiders are not to be underestimated. They possess advanced technology and formidable military capabilities, and they are fiercely protective of their independence and autonomy. Any attempt to intrude upon their territory or disrupt their way of life is likely to be met with swift and brutal retaliation.";        

        protected override float BaseTimeToAttackInSeconds => 6;

        protected override float TimeMultiplier => 1;

        public override List<Resource> BaseRewards => new List<Resource>()
        {
            new CarbonFiberResource(150000),
            new QuantumGlassResource(150000),
            new HiggsBosonResource(50000),

        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new LightFighterShip(50),
            new HeavyFighterShip(25),
        };
        public override List<Research> BaseRequirements => new List<Research>()
        {
            new TheExpanseResearch(3),
            new ArtOfWarResearch(2),
            new HyperdriveResearch(1),
        };
    }
}