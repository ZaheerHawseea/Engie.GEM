using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Entities
{
    /// <summary>
    /// Models a wind power plant.
    /// </summary>
    public class WindPowerPlant : PowerPlant
    {
        /// <summary>
        /// Gets or sets the wind percentage.
        /// </summary>
        /// <remarks>
        /// Value between 0 and 100.
        /// </remarks>
        public decimal WindPercentage { get; set; }

        /// <summary>
        /// Gets the cost to produce 1 MWH using wind.
        /// </summary>
        public override decimal CostPerMWh => 0;

        /// <summary>
        /// Gets the maximum capacity (power) the plant can produce given the wind percentage.
        /// </summary>
        /// <remarks>
        /// Percentage of 0 means no power can be produced, thus Maximum capacity will be 0.
        /// </remarks>
        public override int MaximumCapacity => (int) (this.MaximumPower * (WindPercentage / 100));
    }
}