using QuantumWorld.Core.Domain;

namespace QuantumWorld.Tests.Domain
{
    public class ResearchTests
    {
        [Fact]
        public void Research_UpgradeResearch_Should_Increase_Its_Level_By_One()
        {
            var theExpanseResearch = new TheExpanseResearch();
            theExpanseResearch.UpgradeResearch();

            var expectedLevel = 1;
            var actualLevel = theExpanseResearch.Level;

            Assert.Equal(expectedLevel, actualLevel);
        }

        [Fact]
        public void Research_UpgradeResearch_Should_Increase_Its_Cost_Properly()
        {
            var theExpanseResearch = new TheExpanseResearch();
            theExpanseResearch.UpgradeResearch();

            List<Resource> expectedCost = new List<Resource>()
            {
                new CarbonFiberResource(300),
                new QuantumGlassResource(300)
            };

            List<Resource> actualCost = theExpanseResearch.Cost;

            actualCost.Should().BeEquivalentTo(expectedCost);
        }

        [Fact]
        public void Research_UpgradeResearch_Should_Increase_Its_TimeToBuild_Properly()
        {
            var theExpanseResearch = new TheExpanseResearch();
            theExpanseResearch.UpgradeResearch();

            float expectedTime = 30;

            float actualTime = theExpanseResearch.TimeToBuildInSeconds;

            Assert.Equal(expectedTime, actualTime);
        }
    }
}