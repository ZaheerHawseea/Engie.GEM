using Engie.GEM.ProductionPlan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Engie.GEM.ProductionPlan.Core.Test.EntitiesTests
{
    /// <summary>
    /// Unit test class for <see cref="WindPowerPlant"/>.
    /// </summary>
    public class WindPowerPlantTest
    {
        [Fact]
        public void GivenAWindPercentagPlantShouldProduceAtMaximumCapacity()
        {
            // Arrange
            var powerPlant = new WindPowerPlant() { Name = "Test 1", Efficiency = 60, MinimumPower = 0, MaximumPower = 200, WindPercentage = 60 };
            var expectedCapacity = 120;

            // Act
            var capacity = powerPlant.MaximumCapacity;

            // Assert
            Assert.Equal(expectedCapacity, capacity);
        }
    }
}
