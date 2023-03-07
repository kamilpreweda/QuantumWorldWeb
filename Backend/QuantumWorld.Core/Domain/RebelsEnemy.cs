namespace QuantumWorld.Core.Domain
{
    public class RebelsEnemy : Enemy
    {
        public override string BaseDescription => "The Rebels are a dangerous and extremist faction that has long been in conflict with humanity. They reject the authority of established governments and institutions and seek to overthrow the status quo by any means necessary. Led by charismatic and ruthless leaders, the Rebels have amassed a sizable fleet of warships and are not afraid to use them to further their cause. They are known for their guerrilla tactics and surprise attacks, often targeting civilian populations and infrastructure in an attempt to disrupt the normal functioning of society.";

        protected override float BaseTimeToAttackInSeconds => 80;

        protected override float TimeMultiplier => 1;

        public override List<Resource> BaseRewards => new List<Resource>()
        {
            new CarbonFiberResource(25000),
            new QuantumGlassResource(25000),
            new HiggsBosonResource(10000)
        };

        public override List<Ship> BaseShips => new List<Ship>()
        {
            new Battleship(50),
            new Destroyer(50)
        };
         public override List<Research> BaseRequirements => new List<Research>()
        {
            new TheExpanseResearch(8),
            new ArtOfWarResearch(6),
            new HyperdriveResearch(4),
        };
    }
}