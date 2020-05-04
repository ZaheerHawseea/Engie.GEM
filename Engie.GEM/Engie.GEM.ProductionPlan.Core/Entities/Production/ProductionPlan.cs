using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Entities
{
    /// <summary>
    /// Models the production plan (i.e. a given load and collection of power plants).
    /// </summary>
    public class ProductionPlan
    {
        /// <summary>
        /// Gets or sets the load to be produced.
        /// </summary>
        public int Load { get; set; }

        /// <summary>
        /// Gets or sets the collection of power plants to be used for the production.
        /// </summary>
        public IEnumerable<PowerPlant> PowerPlants { get; set; }
    }
}
