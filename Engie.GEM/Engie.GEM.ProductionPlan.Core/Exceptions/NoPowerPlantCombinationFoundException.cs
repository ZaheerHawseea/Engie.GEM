using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.GEM.ProductionPlan.Core.Exceptions
{
    /// <summary>
    /// Exception class used when no power plant combination can be made to meet the specified load.
    /// </summary>
    public class NoPowerPlantCombinationFoundException : Exception
    {
        public NoPowerPlantCombinationFoundException() { }

        public NoPowerPlantCombinationFoundException(int load): base($"No power plant combination found to meet a load of {load}MWh.") { }

        public NoPowerPlantCombinationFoundException(string message) : base(message) { }

        public NoPowerPlantCombinationFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
