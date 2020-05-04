using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Entities
{
    /// <summary>
    /// Models a kerosine power plant.
    /// </summary>
    public class TurboJetPowerPlant : PowerPlant
    {
        /// <summary>
        /// Gets or sets the price of kerosine.
        /// </summary>
        public decimal KerosinePrice { get; set; }

        /// <summary>
        /// Gets the cost to produce 1 MWH using kerosine.
        /// </summary>
        public override decimal CostPerMWh => (KerosinePrice * 100) / (decimal) this.Efficiency;
    }
}