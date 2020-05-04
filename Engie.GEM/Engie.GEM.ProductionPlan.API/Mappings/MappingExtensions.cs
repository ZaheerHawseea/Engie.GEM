using AutoMapper;
using Engie.GEM.ProductionPlan.API.Exceptions;
using Engie.GEM.ProductionPlan.API.Models;
using Engie.GEM.ProductionPlan.API.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Mappings
{
    /// <summary>
    /// Add extension methods to the mapper <see cref="IMapper"/>.
    /// </summary>
    internal static class MappingExtensions
    {
        /// <summary>
        /// Maps <see cref="ProductionPlanRequest"/> to <see cref="Core.Entities.ProductionPlan"/>
        /// </summary>
        /// <param name="mapper">The <see cref="IMapper"/> instance</param>
        /// <param name="request">The <see cref="ProductionPlanRequest"/> instance</param>
        /// <returns>A new instance of <see cref="Core.Entities.ProductionPlan"/></returns>
        public static Core.Entities.ProductionPlan MapToProductionPlan(this IMapper mapper, ProductionPlanRequest request)
        {
            // Map simple fields
            var plan = mapper.Map<Core.Entities.ProductionPlan>(request);

            // Map the power plants by their type
            plan.PowerPlants = request.PowerPlants.Select<PowerPlant, Core.Entities.PowerPlant>(p => {
                switch (p.Type)
                {
                    case PowerPlantType.GASFIRED:
                        {
                            var powerPlant = mapper.Map<Core.Entities.GasPowerPlant>(p);

                            powerPlant.GasPrice = request.FuelCosting.GasPrice;

                            return powerPlant;
                        }

                    case PowerPlantType.WINDTURBINE:
                        {
                            var powerPlant = mapper.Map<Core.Entities.WindPowerPlant>(p);

                            powerPlant.WindPercentage = request.FuelCosting.WindPercentage;

                            return powerPlant;
                        }

                    case PowerPlantType.TURBOJET:
                        {
                            var powerPlant = mapper.Map<Core.Entities.TurboJetPowerPlant>(p);

                            powerPlant.KerosinePrice = request.FuelCosting.KerosinePrice;

                            return powerPlant;
                        }

                    default:
                        throw new MappingException($"Cannot map power plant {p.Name}");
                }
            });

            return plan;
        }
    }
}
