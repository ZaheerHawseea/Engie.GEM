using Engie.GEM.ProductionPlan.API.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Models
{
    /// <summary>
    /// Models the PowerPlant information of the API.
    /// </summary>
    public class PowerPlant
    {
        /// <summary>
        /// Gets or sets the name of the power plant.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the power plant.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public PowerPlantType Type { get; set; }

        /// <summary>
        /// Gets or sets the efficiency of the power plant (a.k.a. thermal efficiency).
        /// </summary>
        [JsonProperty(PropertyName = "efficiency")]
        public double Efficiency { get; set; }

        /// <summary>
        /// Gets or sets the minimum power that the power plant can generate.
        /// </summary>
        [JsonProperty(PropertyName = "pmin")]
        public int MinimumPower { get; set; }

        /// <summary>
        /// Gets or sets the maximum power that the power plant can generate.
        /// </summary>
        [JsonProperty(PropertyName = "pmax")]
        public int MaximumPower { get; set; }
    }
}
