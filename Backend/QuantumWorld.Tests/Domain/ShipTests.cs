using QuantumWorld.Core.Domain;

namespace QuantumWorld.Tests.Domain
{
    public class ShipTests
    {
        [Fact]
        public void Ship_GetTotalAP_Should_Calculate_AP_Properly()
        {
            var ship = new LightFighterShip(11);
            ship.GetTotalAP();

            var expectedAP = 110;
            var actualAP = ship.GetTotalAP();

            Assert.Equal(expectedAP, actualAP);
        }

        [Fact]
        public void Ship_GetTotalHP_Should_Calculate_HP_Properly()
        {
            var ship = new LightFighterShip(12);
            ship.GetTotalHP();

            var expectedHP = 240;
            var actualHP = ship.GetTotalHP();

            Assert.Equal(expectedHP, actualHP);
        }

        [Fact]
        public void Ship_BuildShip_Should_Increase_Its_Count_By_One()
        {
            var ship = new HeavyFighterShip(1);
            ship.BuildShip();

            var expectedCount = 2;
            var actualCount = ship.Count;

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Ship_SetCount_Should_Set_Proper_Value()
        {
            var ship = new HeavyFighterShip();
            ship.SetCount(99);

            var expectedCount = 99;
            var actualCount = ship.Count;

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Ship_GetCount_Should_Return_Proper_Value()
        {
            var ship = new HeavyFighterShip(45);

            var expectedCount = 45;
            var actualCount = ship.GetCount();

            Assert.Equal(expectedCount, actualCount);
        }
    }
}