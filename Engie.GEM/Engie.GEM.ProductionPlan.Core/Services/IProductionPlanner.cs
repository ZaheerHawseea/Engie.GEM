using Engie.GEM.ProductionPlan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Services
{
    /// <summary>
    /// Given a production plan <see cref="Entities.ProductionPlan"/>, calculates the load each power plant is given to produce.
    /// </summary>
    public interface IProductionPlanner
    {
        /// <summary>
        /// Plans a given load to each power plant <see cref="Entities.ProductionPlan"/>.
        /// </summary>
        /// <param name="productionPlan">The production plan <see cref="Entities.ProductionPlan"/></param>
        /// <returns>Collection of <see cref="PowerPlantLoad"/> giving each power plant a specific load to produce.</returns>
        IEnumerable<PowerPlantLoad> Plan(Entities.ProductionPlan productionPlan);
    }
}