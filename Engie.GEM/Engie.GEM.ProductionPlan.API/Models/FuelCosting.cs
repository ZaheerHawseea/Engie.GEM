using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Models
{
    /// <summary>
    /// Models the API fuel information.
    /// </summary>
    public class FuelCosting
    {
        /// <summary>
        /// Gets or sets the gas price per MWH.
        /// </summary>
        [JsonProperty(PropertyName = "gas(euro/MWh)")]
        public decimal GasPrice { get; set; }

        /// <summary>
        /// Gets or sets the kerosine price per MWH.
        /// </summary>
        [JsonProperty(PropertyName = "kerosine(euro/MWh)")]
        public decimal KerosinePrice { get; set; }

        /// <summary>
        /// Gets or sets the CO2Price per ton.
        /// </summary>
        [JsonProperty(PropertyName = "co2(euro/ton)")]
        public decimal CO2Price { get; set; }

        /// <summary>
        /// Gets or sets the WindPercentage.
        /// </summary>
        /// <remarks>
        /// Value between 0 and 100.
        /// </remarks>
        [JsonProperty(PropertyName = "wind(%)")]
        public decimal WindPercentage { get; set; }
    }
}
