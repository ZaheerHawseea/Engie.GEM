using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Entities
{
    /// <summary>
    /// Models a Gas power plant.
    /// </summary>
    public class GasPowerPlant : PowerPlant
    {
        /// <summary>
        /// Gets or sets the price of gas.
        /// </summary>
        public decimal GasPrice { get; set; }

        /// <summary>
        /// Gets the cost to produce 1 MWH using gas.
        /// </summary>
        public override decimal CostPerMWh => (GasPrice * 100) / (decimal) this.Efficiency;
    }
}