namespace QuantumWorld.Core.Domain
{
    public class DistantsEnemy : Enemy
    {
        public override string Description => "Distants Enemy Description";

        protected override TimeSpan BaseTimeToAttack => TimeSpan.FromSeconds(120);

        protected override float TimeMultiplier => 1;

        public override List<Resource> BaseRewards => new List<Resource>()
        {
            new CarbonFiberResource(100000),
            new QuantumGlassResource(100000),
            new HiggsBosonResource(100000)
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new Battleship(200),
            new Destroyer(150),
            new Dreadnought(100)
        };
         public override List<Research> BaseRequirements => new List<Research>()
        {
            new TheExpanseResearch(12),
            new ArtOfWarResearch(10),
            new HyperdriveResearch(8),
        };
    }
}