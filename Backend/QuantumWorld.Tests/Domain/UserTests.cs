using System.Text;
using QuantumWorld.Core.Domain;
using MongoDB.Bson;

namespace QuantumWorld.Tests.Domain
{
    public class UserTests
    {
        public User SetUser()
        {
            List<Resource> resources = new List<Resource>()
            {
                new CarbonFiberResource(),
                new QuantumGlassResource(),
                new HiggsBosonResource()
            };

            List<Building> buildings = new List<Building>()
            {
                new CarbonFiberFactory(),
                new QuantumGlassFactory(),
                new HiggsBosonDetector(),
                new Labolatory(),
                new SpaceshipFactory(),
            };

            List<Research> research = new List<Research>()
            {
                new TheExpanseResearch(),
                new ArtOfWarResearch(),
                new HyperdriveResearch(),
                new TerraformingResearch(),
            };

            List<Ship> ships = new List<Ship>()
            {
                new LightFighterShip(),
                new HeavyFighterShip(),
                new Battleship(),
                new Destroyer(),
                new Dreadnought(),
                new Mothership(),
            };

            List<Enemy> enemies = new List<Enemy>()
            {
                new PiratesEnemy(),
                new OutsidersEnemy(),
                new RebelsEnemy(),
                new ArmamentsEnemy(),
                new DistantsEnemy(),
                new AncientsEnemy()
            };

            Battle battle = new Battle();
            var id = Guid.NewGuid();          


            User user = new User(id, Encoding.ASCII.GetBytes("12345"), Encoding.ASCII.GetBytes("12345"), "testUser");

            return user;
        }

        [Fact]
        public void User_UpgradeBuilding_Should_Upgrade_Correct_Building_Based_On_Its_Type()
        {
            var user = SetUser();
            user.UpgradeBuilding(BuildingType.CarbonFiberFactory);

            float expectedTime = 4;
            List<Resource> expectedCost = new List<Resource>()
            {
                new CarbonFiberResource(300),
                new QuantumGlassResource(100)
            };
            int expectedLevel = 1;

            float actualTime = user.Buildings.SingleOrDefault(b => b.Name == "CarbonFiberFactory").TimeToBuildInSeconds;
            List<Resource> actualCost = user.Buildings.SingleOrDefault(b => b.Name == "CarbonFiberFactory").Cost;
            int actualLevel = user.Buildings.SingleOrDefault(b => b.Name == "CarbonFiberFactory").Level;

            Assert.Equal(expectedTime, actualTime);
            actualCost.Should().BeEquivalentTo(expectedCost);
            Assert.Equal(expectedLevel, actualLevel);
        }

        [Fact]
        public void User_UpgradeBuilding_Spend_Proper_Values_Of_Resources()
        {
            var user = SetUser();
            user.UpgradeBuilding(BuildingType.QuantumGlassFactory);

            List<Resource> expectedPlayerResources = new List<Resource>()
            {
                new CarbonFiberResource(400),
                new QuantumGlassResource(400)
            };

            List<Resource> actualPlayerResources = user.Resources;

            expectedPlayerResources.Should().BeEquivalentTo(expectedPlayerResources);
        }

        [Fact]
        public void User_UpgradeBuilding_Should_Increase_Used_Space()
        {
            var user = SetUser();
            user.UpgradeBuilding(BuildingType.CarbonFiberFactory);

            int expectedUsedSpace = 1;
            int actualUsedSpace = user.UsedSpace;
            Assert.Equal(expectedUsedSpace, actualUsedSpace);

        }
        [Fact]
        public void User_UpgradeBuilding_Should_Calculate_Points_Properly()
        {
            var user = SetUser();
            user.UpgradeBuilding(BuildingType.SpaceshipFactory);

            double expectedResult = 0.9;
            double actualResult = user.Points;
            Assert.Equal(expectedResult, actualResult);

        }

        [Fact]
        public void User_UpgradeResearch_Should_Upgrade_Correct_Research_Based_On_Its_Type()
        {
            var user = SetUser();
            user.Buildings.Where(b => b.Type == BuildingType.Labolatory).FirstOrDefault().SetLevelForTests(12);
            user.UpgradeResearch(ResearchType.ArtOfWarResearch);

            TimeSpan expectedTime = TimeSpan.FromSeconds(80);
            List<Resource> expectedCost = new List<Resource>()
            {
                new CarbonFiberResource(800),
                new QuantumGlassResource(800)
            };
            int expectedLevel = 1;

            TimeSpan actualTime = user.Research.SingleOrDefault(r => r.Name == "ArtOfWarResearch").TimeToBuild;
            List<Resource> actualCost = user.Research.SingleOrDefault(r => r.Name == "ArtOfWarResearch").Cost;
            int actualLevel = user.Research.SingleOrDefault(r => r.Name == "ArtOfWarResearch").Level;

            Assert.Equal(expectedTime, actualTime);
            actualCost.Should().BeEquivalentTo(expectedCost);
            Assert.Equal(expectedLevel, actualLevel);
        }

        [Fact]
        public void User_BuildShip_Should_Build_Correct_Ship_Based_On_Its_Type()
        {
            var user = SetUser();
            user.Buildings.Where(b => b.Type == BuildingType.SpaceshipFactory).FirstOrDefault().SetLevelForTests(12);

            user.BuildShip(ShipType.LightFighterShip, 1);

            int expectedShipCount = 1;
            int actualShipCount = user.Ships.SingleOrDefault(s => s.Name == "LightFighterShip").Count;

            Assert.Equal(expectedShipCount, actualShipCount);
        }

        [Fact]
        public void User_Start_Battle_Should_Choose_Correct_Enemy_Based_On_Its_Type()
        {
            var user = SetUser();

            user.Ships = new List<Ship>()
            {
                new LightFighterShip(100),
                new HeavyFighterShip(100)
            };
            var enemy = new PiratesEnemy();

            user.StartBattle(enemy.Type);


            bool expectedResult = true;
            bool actualResult = user.Enemies.SingleOrDefault(e => e.Type == enemy.Type).IsDefeated;

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void User_Start_Battle_Should_Increase_Number_Of_Defeated_Enemies_If_Won()
        {
            var user = SetUser();

            user.Ships = new List<Ship>()
            {
                new LightFighterShip(1000),
                new HeavyFighterShip(1000)
            };
            var enemy = new PiratesEnemy();

            user.StartBattle(enemy.Type);
            int expectedResult = 1;
            int actualResult = user.EnemiesDefeated;
            Assert.Equal(expectedResult, actualResult);
        }
    }
}