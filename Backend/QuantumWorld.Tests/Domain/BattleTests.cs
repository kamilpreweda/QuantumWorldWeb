using QuantumWorld.Core.Domain;

namespace QuantumWorld.Tests.Domain
{
    public class BattleTests
    {
        [Fact]
        public void Battle_Attack_Should_Subtract_TotalAp_From_TotalHp()
        {
            var battle = new Battle();
            int totalAp = 11;
            int totalHp = 5;

            var expectedResult = -6;
            var actualResult = battle.Attack(totalAp, totalHp);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Battle_CollectRewards_Should_Return_Proper_List_Of_Resource()
        {
            var battle = new Battle();
            var pirates = new PiratesEnemy();

            var expectedResult = pirates.GetRewards();
            var actualResult = battle.CollectRewards(pirates);

            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Fact]
        public void Battle_CalculateDestroyedShips_Shoud_Calculate_Damage_Properly()
        {
            var battle = new Battle();
            int damage = 170;

            var lightFighterShip = new LightFighterShip();
            var heavyFighterShip = new HeavyFighterShip();

            lightFighterShip.SetCount(3);
            heavyFighterShip.SetCount(2);

            var ships = new List<Ship>()
            {
                lightFighterShip,
                heavyFighterShip
            };

            int expectedResult = 10;
            battle.CalculateDestroyedShips(ships, damage, out int remainingDamage);

            Assert.Equal(expectedResult, remainingDamage);
        }

        [Fact]
        public void Battle_CalculateDestroyedShips_Shoud_Calculate_Ship_Count_Properly()
        {
            var battle = new Battle();
            int damage = 150;

            var lightFighterShip = new LightFighterShip();
            var heavyFighterShip = new HeavyFighterShip();

            lightFighterShip.SetCount(3);
            heavyFighterShip.SetCount(2);

            var ships = new List<Ship>()
            {
                lightFighterShip,
                heavyFighterShip
            };

            int expectedLightFighterCount = 0;
            int expectedHeavyFighterCount = 1;
            battle.CalculateDestroyedShips(ships, damage, out int remainingDamage);

            Assert.Equal(expectedLightFighterCount, lightFighterShip.Count);
            Assert.Equal(expectedHeavyFighterCount, heavyFighterShip.Count);
        }

        [Fact]
        public void Battle_Get_Total_AP_Should_Calculate_AP_Properly()
        {
            var battle = new Battle();
            var lightFighterShip = new LightFighterShip();
            var heavyFighterShip = new HeavyFighterShip();

            lightFighterShip.SetCount(5);
            heavyFighterShip.SetCount(10);

            var ships = new List<Ship>()
            {
            lightFighterShip,
            heavyFighterShip
            };

            var expectedResult = 350;
            var actualResult = battle.GetTotalAP(ships);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Battle_Get_Total_HP_Should_Calculate_HP_Properly()
        {
            var battle = new Battle();
            var lightFighterShip = new LightFighterShip();
            var heavyFighterShip = new HeavyFighterShip();

            lightFighterShip.SetCount(7);
            heavyFighterShip.SetCount(2);

            var ships = new List<Ship>()
            {
            lightFighterShip,
            heavyFighterShip
            };

            var expectedResult = 240;
            var actualResult = battle.GetTotalHP(ships);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Battle_StartBattle_Should_Calculate_Proper_Ship_Counts_For_Player_And_Enemy()
        {
            var battle = new Battle();

            var playerLightFighter = new LightFighterShip();
            var playerHeavyFighter = new HeavyFighterShip();
            playerLightFighter.SetCount(10);
            playerHeavyFighter.SetCount(10);
            var playerShips = new List<Ship>()
                {
                    playerLightFighter,
                    playerHeavyFighter
                };

            var piratesEnemy = new PiratesEnemy();

            var playerResources = new List<Resource>();

            battle.StartBattle(playerShips, playerResources, piratesEnemy);

            var expectedPlayerLightFighterCount = 1;
            var expectedPlayerHeavyFighterCount = 10;

            var expectedEnemyLightFighterCount = 0;
            var expectedEnemyHeavyFighterCount = 0;

            Assert.Equal(expectedPlayerLightFighterCount, playerLightFighter.Count);
            Assert.Equal(expectedPlayerHeavyFighterCount, playerHeavyFighter.Count);

            var remainingEnemyShips = piratesEnemy.GetShips();

            Assert.Equal(expectedEnemyLightFighterCount, remainingEnemyShips.SingleOrDefault(s => s.Type == ShipType.LightFighterShip).Count);
            Assert.Equal(expectedEnemyHeavyFighterCount, remainingEnemyShips.SingleOrDefault(s => s.Type == ShipType.LightFighterShip).Count);

        }

        [Fact]
        public void Battle_Assign_Should_Add_Correct_Resources_Values_To_PlayerResources()
        {
            var playerResources = new List<Resource>()
            {
                new CarbonFiberResource(6000),
                new QuantumGlassResource(6000),
            };

            var battle = new Battle();
            var piratesEnemy = new PiratesEnemy();
            var rewards = piratesEnemy.GetRewards();

            battle.AssignRewards(playerResources, rewards);

            var expectedCarbonFiberValue = 11000;
            var expectedQuantumGlassValue = 11000;

            var actualCarbonFiberValue = playerResources.SingleOrDefault(r => r.Type == ResourceType.CarbonFiberResource).Value;
            var actualQuantumGlassValue = playerResources.SingleOrDefault(r => r.Type == ResourceType.QuantumGlassResource).Value;

            Assert.Equal(expectedCarbonFiberValue, actualCarbonFiberValue);
            Assert.Equal(expectedQuantumGlassValue, actualQuantumGlassValue);   
        }
    }
}