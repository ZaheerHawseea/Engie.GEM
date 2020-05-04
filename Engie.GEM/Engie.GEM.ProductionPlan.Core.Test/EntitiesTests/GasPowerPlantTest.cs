using Engie.GEM.ProductionPlan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Engie.GEM.ProductionPlan.Core.Test.EntitiesTests
{
    /// <summary>
    /// Unit test class for <see cref="GasPowerPlant"/>.
    /// </summary>
    public class GasPowerPlantTest
    {
        [Fact]
        public void GivenGasPriceAndEfficiencyCostPerMWHShouldBeCalculated()
        {
            // Arrange
            var powerPlant = new GasPowerPlant() { Name = "Test 2", Efficiency = 50, MinimumPower = 0, MaximumPower = 200, GasPrice = 12.5M };
            var expectedPricePerMWH = 25.0M;

            // Act
            var pricePerMWH = powerPlant.CostPerMWh;

            // Assert
            Assert.Equal(expectedPricePerMWH, pricePerMWH);
        }
    }
}
