using Engie.GEM.ProductionPlan.Core.Configuration;
using Engie.GEM.ProductionPlan.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Configuration
{
    /// <summary>
    /// Defines extension methods to the <see cref="IServiceCollection"/> class.
    /// Used for registering dependencies of the API.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the dependencies of the API.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance</param>
        /// <returns>The <see cref="IServiceCollection"/> instance</returns>
        public static IServiceCollection AddEngieAPI(this IServiceCollection services)
        {
            services.AddEngieCore();

            return services;
        }
    }
}