using Engie.GEM.ProductionPlan.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Configuration
{
    /// <summary>
    /// Extension class for registering dependencies of the core.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the dependencies of the core to a service collection.
        /// </summary>
        /// <param name="services">The service collection <see cref="IServiceCollection"/> to which to add the dependencies.</param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddEngieCore(this IServiceCollection services) 
        {
            services.AddScoped<IProductionPlanner, EngiePriorityListProductionPlanner>();

            return services;
        }
    }
}
