using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Models
{
    /// <summary>
    /// Models the API result by giving the load/power for a specific power plant.
    /// </summary>
    public class PowerPlantResult
    {
        /// <summary>
        /// Gets or sets the name of the power plant.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string PowerPlantName { get; set; }

        /// <summary>
        /// Gets or sets the power/load assigned to the power plant.
        /// </summary>
        [JsonProperty(PropertyName = "p")]
        public int Power { get; set; }

        /// <summary>
        /// Test for Equality by comparing the plant name in the result object.
        /// </summary>
        /// <param name="obj">The other <see cref="PowerPlantResult"/> to compare with</param>
        /// <returns>True if result contain same name, else false.</returns>
        public override bool Equals(object obj)
        {
            return PowerPlantName.Equals((obj as PowerPlantResult).PowerPlantName);
        }

        /// <summary>
        /// Computes the object hash code. Good practice to implement this method also when Equals() is overriden.
        /// </summary>
        /// <returns>The computed hash code of the object.</returns>
        public override int GetHashCode()
        {
            return PowerPlantName.GetHashCode();
        }
    }
}