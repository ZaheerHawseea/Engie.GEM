using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Engie.GEM.ProductionPlan.API.Mappings;
using Engie.GEM.ProductionPlan.API.Models;
using Engie.GEM.ProductionPlan.Core.Entities;
using Engie.GEM.ProductionPlan.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Engie.GEM.ProductionPlan.API.Controllers
{
    /// <summary>
    /// API controller class for /PowerProduction endpoint.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PowerProductionController : ControllerBase
    {
        private readonly ILogger<PowerProductionController> logger;
        private readonly IProductionPlanner productionPlanner;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="productionPlanner">An instance of <see cref="IProductionPlanner"/> for processing the core logics.</param>
        /// <param name="mapper">An instance of <see cref="IMapper"/> for mapping the API models to the Core entities.</param>
        /// <param name="logger">An instance of <see cref="ILogger"/> for logging purposes.</param>
        public PowerProductionController(IProductionPlanner productionPlanner, IMapper mapper, ILogger<PowerProductionController> logger)
        {
            this.productionPlanner = productionPlanner;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// POST method for processing a production plan.
        /// </summary>
        /// <param name="productionPlanRequest">The <see cref="ProductionPlanRequest"/> containing the load and power plants information.</param>
        /// <returns>Collection of <see cref="PowerPlantResult"/></returns>
        [HttpPost]
        public IEnumerable<PowerPlantResult> Post(ProductionPlanRequest productionPlanRequest)
        {
            // Map DTO to core entity
            var plan = mapper.MapToProductionPlan(productionPlanRequest);

            // Execute core logics
            var result = productionPlanner.Plan(plan);

            // Create initial results which is combined with result return from core.
            // This is to make sure power plants that generate no power is also returned in the api response. 
            // The core does not return power plants that do not generate power.
            var initialResults = productionPlanRequest.PowerPlants.Select(p => new PowerPlantResult() { PowerPlantName =  p.Name, Power = 0});

            // Map and return result.
            return result.Select(p => mapper.Map<Models.PowerPlantResult>(p)).Union(initialResults); 
        }
    }
}
