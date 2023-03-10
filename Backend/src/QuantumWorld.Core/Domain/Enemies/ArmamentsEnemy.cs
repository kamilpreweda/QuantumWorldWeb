namespace QuantumWorld.Core.Domain
{
    public class ArmamentsEnemy : Enemy
    {
        public override string BaseDescription => "The Armaments are a highly trained and well-equipped military organization that operates outside of the normal chain of command. Made up of the most elite soldiers from various nations and factions, they are feared and respected throughout the galaxy for their deadly precision and unwavering loyalty to their cause. Engaging the Armaments requires careful planning and preparation, as they are known for their discipline and tactics.";

        protected override float BaseTimeToAttackInSeconds => 8;

        protected override float TimeMultiplier => 1;

        public override List<Resource> BaseRewards => new List<Resource>()
        {
            new CarbonFiberResource(50000),
            new QuantumGlassResource(50000),
            new HiggsBosonResource(50000)
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new Destroyer(100),
            new Dreadnought(50)
        };
        public override List<Research> BaseRequirements => new List<Research>()
        {
            new TheExpanseResearch(10),
            new ArtOfWarResearch(8),
            new HyperdriveResearch(6),
        };
    }
}