using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Entities
{
    /// <summary>
    /// Models the load/power a specific power plant is given to produce.
    /// </summary>
    public class PowerPlantLoad
    {
        /// <summary>
        /// Gets or sets the power plant <see cref="PowerPlant"/>.
        /// </summary>
        public PowerPlant PowerPlant { get; set; }

        /// <summary>
        /// Gets or sets the load/power to be produced by the power plant.
        /// </summary>
        public int Load { get; set; }
    }
}