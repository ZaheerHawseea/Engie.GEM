using Engie.GEM.ProductionPlan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Engie.GEM.ProductionPlan.Core.Test.EntitiesTests
{
    /// <summary>
    /// Unit test class for <see cref="TurboJetPowerPlant"/>.
    /// </summary>
    public class TurboJetPowerPlantTest
    {
        [Fact]
        public void GivenKerosinePriceAndEfficiencyCostPerMWHShouldBeCalculated()
        {
            // Arrange
            var powerPlant = new TurboJetPowerPlant() { Name = "Test 3", Efficiency = 30, MinimumPower = 0, MaximumPower = 200, KerosinePrice = 15.0M };
            var expectedPricePerMWH = 50.0M;

            // Act
            var pricePerMWH = powerPlant.CostPerMWh;

            // Assert
            Assert.Equal(expectedPricePerMWH, pricePerMWH);
        }
    }
}
