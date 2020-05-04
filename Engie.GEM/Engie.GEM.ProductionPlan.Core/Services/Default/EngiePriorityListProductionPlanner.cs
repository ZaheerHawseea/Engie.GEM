using Engie.GEM.ProductionPlan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Engie.GEM.ProductionPlan.Core.Exceptions;

namespace Engie.GEM.ProductionPlan.Core.Services
{
    /// <summary>
    /// Default implementation of <see cref="IProductionPlanner"/> using PriorityList method to solve the Unit Commitment problem.
    /// </summary>
    internal class EngiePriorityListProductionPlanner : IProductionPlanner
    {
        /// <summary>
        /// Plans a given load to each power plant <see cref="Entities.ProductionPlan"/>.
        /// </summary>
        /// <param name="productionPlan">The production plan <see cref="Entities.ProductionPlan"/></param>
        /// <returns>Collection of <see cref="PowerPlantLoad"/> giving each power plant a specific load to produce.</returns>
        public IEnumerable<PowerPlantLoad> Plan(Entities.ProductionPlan productionPlan)
        {
            // Generate priority list table
            var priorityList = CreatePriorityList(productionPlan.PowerPlants, productionPlan.Load);

            // Raise exception if no combination can be made
            if (priorityList.Count() == 0)
            {
                throw new NoPowerPlantCombinationFoundException(productionPlan.Load);
            }

            // Dispatch the load (a.k.a. Economic Load Dispatch problem)
            return DispatchLoad(priorityList, productionPlan.Load);
        }

        /// <summary>
        /// Creates a table of the possible comibinations of power plants taking into consideration their merit order (or priority) based on the cost of producing energy.
        /// Also calculates the combined minimum and maximum power of each combination
        /// 
        /// For e.g. If there are 3 power plants A, B, C and each have the following merit order and power range:
        ///     PowerPlant   - Range        - Merit Order / Priority
        ///     PowerPlant A - [0 - 100]    - 3
        ///     PowerPlant B - [50 - 200]   - 2
        ///     PowerPlant C - [100 - 250]  - 1
        ///     
        /// Then the table of the possible combinations generated will be:
        ///     Combination,    Combined Range
        ///     [C],            [100 - 250]
        ///     [C, B],         [150 - 450]
        ///     [C, B, A],      [150 - 550]
        ///     
        ///     Note: Certain combinations won't be returned if it cannot meet the load parameter. For e.g. if load is 400, then [C] won't be returned since it has 
        ///     a maximum combined power of 250.
        /// </summary>
        /// <param name="powerPlants">Collection of <see cref="PowerPlant"/> used to generate the priority table</param>
        /// <param name="load">The load used to calculate possible combinations of power plants to be used</param>
        /// <returns>Collection of <see cref="PowerPlantCombination"/></returns>
        private IEnumerable<PowerPlantCombination> CreatePriorityList(IEnumerable<PowerPlant> powerPlants, int load)
        {
            // Order by the cheapest (merit order)
            var orderedPowerPlants = powerPlants.Where(p => p.MaximumCapacity > 0).OrderBy(p => p.CostPerMWh);

            // Create priority table by starting with the cheapest
            var powerPlantCount = powerPlants.Count();
            var powerPlantCombinations = new List<PowerPlantCombination>(powerPlantCount); // Assign capacity, so Add() is o(1).
            var powerPlantCombination = new List<PowerPlant>(powerPlantCount); // A single combination
            var combinedMinimumPower = 0;
            var combinedMaximumPower = 0;

            foreach (var powerPlant in orderedPowerPlants)
            {
                combinedMinimumPower += powerPlant.MinimumPower;
                combinedMaximumPower += powerPlant.MaximumCapacity;

                powerPlantCombination.Add(powerPlant);

                // Restrict to only those combinations that can meet the load.
                if (combinedMinimumPower <= load && combinedMaximumPower >= load)
                {
                    powerPlantCombinations.Add(new PowerPlantCombination() { PowerPlants = new List<PowerPlant>(powerPlantCombination), MinimumCombinedPower = combinedMinimumPower, MaximumCombinedPower = combinedMaximumPower });
                }
            }

            return powerPlantCombinations;
        }

        /// <summary>
        /// Dispatch the load to the list of <see cref="PowerPlantCombination"/>
        /// </summary>
        /// <remarks>
        /// This problem can be solved using the mathematical method known as Langarian method. However given this is a simpler implementation
        /// without complex cost curves, a simpler approach is used where we set all power plants in a combination to it's minimum power
        /// then distribute the remaining load from the cheapest power plant to the most expensive.
        /// </remarks>
        /// <param name="priorityList">The collection of <see cref="PowerPlantCombination"/></param>
        /// <param name="load">The load to be dispatched</param>
        /// <returns>Collection of <see cref="PowerPlantLoad"/> specify the load for each power plant</returns>
        private IEnumerable<PowerPlantLoad> DispatchLoad(IEnumerable<PowerPlantCombination> priorityList, int load)
        {
            // Distribute load & calculate cost for each
            var powerPlantCombinationsAndCosts = new List<(List<PowerPlantLoad> PowerPlantLoads, decimal TotalCost)>(priorityList.Count());

            foreach (var powerPlantCombination in priorityList)
            {
                // Each power plant will receive a minimum of the load
                var remainingLoad = load - powerPlantCombination.MinimumCombinedPower;
                var powerPlantLoadList = new List<PowerPlantLoad>(powerPlantCombination.PowerPlants.Count());
                var totalCost = 0.0m;

                // Distribute the remaining load in order of the cheapest plant
                foreach (var powerPlant in powerPlantCombination.PowerPlants)
                {
                    var power = powerPlant.MinimumPower;

                    // Only distribute remaining load if above 0 else take the minimum power
                    if (remainingLoad > 0)
                    {
                        if (remainingLoad <= (powerPlant.MaximumCapacity - powerPlant.MinimumPower))
                        {
                            power += remainingLoad;
                            remainingLoad = 0;
                        }
                        else
                        {
                            power = powerPlant.MaximumCapacity;
                            remainingLoad -= (powerPlant.MaximumCapacity - powerPlant.MinimumPower);
                        }
                    }

                    // Increment total cost for producting the power with the current combination
                    totalCost += powerPlant.CalculateCost(power);

                    powerPlantLoadList.Add(new PowerPlantLoad{ PowerPlant = powerPlant, Load = power });
                }

                powerPlantCombinationsAndCosts.Add((powerPlantLoadList, totalCost));
            }

            // return the cheapest combination
            return powerPlantCombinationsAndCosts.OrderBy(p => p.TotalCost).FirstOrDefault().PowerPlantLoads;
        }

        /// <summary>
        /// Models a possible combination of power plants that can be used for power production.
        /// </summary>
        internal class PowerPlantCombination
        {
            /// <summary>
            /// Gets or sets the collection of <see cref="PowerPlant"/> in the combination.
            /// </summary>
            /// <remarks>
            /// The collection is ordered by the cheapest power plant being first
            /// </remarks>
            public IEnumerable<PowerPlant> PowerPlants { get; set; }

            /// <summary>
            /// Gets or sets the combined minimum power the power plants in the combination can produce.
            /// </summary>
            public int MinimumCombinedPower { get; set; }

            /// <summary>
            /// Gets or sets the combined maximum power the power plants in the combination can produce.
            /// </summary>
            public int MaximumCombinedPower { get; set; }
        }
    }
}