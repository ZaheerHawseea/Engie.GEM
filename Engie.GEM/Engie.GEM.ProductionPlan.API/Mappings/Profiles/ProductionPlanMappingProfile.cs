using AutoMapper;
using Engie.GEM.ProductionPlan.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Mappings.Profiles
{
    /// <summary>
    /// Mapping profile for conversion between the API models and the Engie.Core entities.
    /// </summary>
    internal class ProductionPlanMappingProfile : Profile
    {
        public ProductionPlanMappingProfile()
        {
            CreateMap<ProductionPlanRequest, Core.Entities.ProductionPlan>()
                .ForMember(p => p.PowerPlants, act => act.Ignore());

            CreateMap<PowerPlant, Core.Entities.WindPowerPlant>();
            CreateMap<PowerPlant, Core.Entities.GasPowerPlant>();
            CreateMap<PowerPlant, Core.Entities.TurboJetPowerPlant>();
            
            CreateMap<Core.Entities.PowerPlantLoad, PowerPlantResult>()
                .ForMember(p => p.PowerPlantName, o => o.MapFrom(s => s.PowerPlant.Name))
                .ForMember(p => p.Power, o => o.MapFrom(s => s.Load));
        }
    }
}
