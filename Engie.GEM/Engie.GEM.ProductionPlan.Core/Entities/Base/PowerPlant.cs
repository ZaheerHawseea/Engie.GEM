using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Entities
{
    /// <summary>
    /// Models a base PowerPlant.
    /// </summary>
    public abstract class PowerPlant
    {
        /// <summary>
        /// Gets or sets the name of the plant.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the efficiency of the plant (a.k.a. Thermal Efficiency).
        /// </summary>
        public double Efficiency { get; set; }

        /// <summary>
        /// Gets or sets the minimum power that the plant can generate.
        /// </summary>
        public int MinimumPower { get; set; }

        /// <summary>
        /// Gets or sets the maximum power that the plant can generate.
        /// </summary>
        public int MaximumPower { get; set; }

        /// <summary>
        /// Gets the maximum capacity (power) the plant can produce given certain constraints.
        /// </summary>
        public virtual int MaximumCapacity => MaximumPower;

        /// <summary>
        /// Gets the cost to produce 1 MWH
        /// </summary>
        public abstract decimal CostPerMWh { get; }

        /// <summary>
        /// Calculates the cost to produce a certain load/power.
        /// </summary>
        /// <param name="power">The power to be produced in MWH</param>
        /// <returns>The cost of production</returns>
        public virtual decimal CalculateCost(int power) => CostPerMWh * power;
    }
}