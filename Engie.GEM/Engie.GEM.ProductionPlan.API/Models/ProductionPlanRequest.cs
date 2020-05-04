using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Models
{
    /// <summary>
    /// Models the API request for a production plan.
    /// </summary>
    public class ProductionPlanRequest
    {
        /// <summary>
        /// Gets or  sets the load to be produced/planned.
        /// </summary>
        [JsonProperty(PropertyName = "load")]
        public int Load { get; set; }

        /// <summary>
        /// Gets or sets the fuel information <see cref="FuelCosting"/>.
        /// </summary>
        [JsonProperty(PropertyName = "fuels")]
        public FuelCosting FuelCosting { get; set; }

        /// <summary>
        /// Gets or sets the collection of power plants <see cref="PowerPlant"/>.
        /// </summary>
        [JsonProperty(PropertyName = "powerplants")]
        public IEnumerable<PowerPlant> PowerPlants { get; set; }
    }
}
