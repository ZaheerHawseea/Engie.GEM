using Engie.GEM.ProductionPlan.Core.Configuration;
using Engie.GEM.ProductionPlan.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Test.Fixtures
{
    /// <summary>
    /// Fixture class used for registering dependencies for the unit tests.
    /// </summary>
    public class ProductionPlannerFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public ProductionPlannerFixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddEngieCore();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }        
    }
}