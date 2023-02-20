namespace QuantumWorld.Core.Domain
{
    public class PiratesEnemy : Enemy
    {
        public override string BaseDescription => "Pirates are a common threat in the galaxy, preying on unsuspecting travelers and raiding valuable cargo. While they may not be the most powerful or advanced foes out there, they can still pose a serious threat to those who underestimate them. ";

        protected override TimeSpan BaseTimeToAttack => TimeSpan.FromSeconds(30);

        protected override float TimeMultiplier => 1;

        public override List<Resource> BaseRewards => new List<Resource>()
        {
            new CarbonFiberResource(5000),
            new QuantumGlassResource(5000),
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new LightFighterShip(10),
            new HeavyFighterShip(10),
        };
        public override List<Research> BaseRequirements => new List<Research>()
        {
            new TheExpanseResearch(4),
            new ArtOfWarResearch(2),
        };
    }
}