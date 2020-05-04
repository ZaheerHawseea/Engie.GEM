using Engie.GEM.ProductionPlan.Core.Entities;
using Engie.GEM.ProductionPlan.Core.Services;
using Engie.GEM.ProductionPlan.Core.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace Engie.GEM.ProductionPlan.Core.Test.ServiceTests
{
    /// <summary>
    /// Unit test class for <see cref="IProductionPlanner"/>
    /// </summary>
    public class ProductionPlannerTest : IClassFixture<ProductionPlannerFixture>
    {
        private readonly IProductionPlanner productionPlanner;

        public ProductionPlannerTest(ProductionPlannerFixture fixture)
        {
            productionPlanner = fixture.ServiceProvider.GetService<IProductionPlanner>();
        }

        [Fact]
        public void GivenAProductionPlanShouldDispatchLoad()
        {
            // Arrange
            var gasPlant = new GasPowerPlant() { Name = "Power Plant #1", Efficiency = 50, GasPrice = 12.5M, MinimumPower = 50, MaximumPower = 150 };
            var windPlant = new WindPowerPlant() { Name = "Power Plant #2", Efficiency = 0, WindPercentage = 75, MinimumPower = 0, MaximumPower = 100 };
            var turboJetPlant = new TurboJetPowerPlant() { Name = "Power Plant #3", Efficiency = 25, KerosinePrice = 25M, MinimumPower = 100, MaximumPower = 300 };
            var expectedLoadForWindPlant = 75;      // Based on MaximumPower * WindPercentage
            var expectedLoadForGasPlant = 75;       // (250 - 75 - 100); Cannot be set to the max since the turbo jet has a minimum of 100.
            var expectedLoadForTurboJetPlant = 100; // Need to match minimum load

            var productionPlan = new Core.Entities.ProductionPlan() 
            { 
                Load = 250,
                PowerPlants = new List<PowerPlant> { gasPlant, windPlant, turboJetPlant }
            };

            // Act
            var powerPlantLoads = new List<PowerPlantLoad>(productionPlanner.Plan(productionPlan));

            // Assert
            Assert.Equal(3, powerPlantLoads.Count);
            
            Assert.Same(windPlant, powerPlantLoads[0].PowerPlant);
            Assert.Equal(expectedLoadForWindPlant, powerPlantLoads[0].Load);

            Assert.Same(gasPlant, powerPlantLoads[1].PowerPlant);
            Assert.Equal(expectedLoadForGasPlant, powerPlantLoads[1].Load);

            Assert.Same(turboJetPlant, powerPlantLoads[2].PowerPlant);
            Assert.Equal(expectedLoadForTurboJetPlant, powerPlantLoads[2].Load);
        }
    }
}
