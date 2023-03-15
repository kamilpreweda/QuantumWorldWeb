namespace QuantumWorld.Core.Domain
{
    public class DistantsEnemy : Enemy
    {
        public override string BaseDescription => "The Distants are a mysterious and enigmatic race that dwell in the far reaches of space, beyond the borders of known human civilization. Little is known about them, as few have ever ventured so far into the void, and those who have returned speak of strange and unsettling encounters. Traveling that far into space requires advanced technology and research, as well as the ability to navigate through the hazards of deep space.";

        protected override float BaseTimeToAttackInSeconds => 9;

        protected override float TimeMultiplier => 1;

        public override List<Resource> BaseRewards => new List<Resource>()
        {
            new CarbonFiberResource(1000000),
            new QuantumGlassResource(1000000),
            new HiggsBosonResource(1000000)
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new Battleship(200),
            new Destroyer(150),
            new Dreadnought(100)
        };
        public override List<Research> BaseRequirements => new List<Research>()
        {
            new TheExpanseResearch(6),
            new ArtOfWarResearch(5),
            new HyperdriveResearch(4),
        };
    }
}