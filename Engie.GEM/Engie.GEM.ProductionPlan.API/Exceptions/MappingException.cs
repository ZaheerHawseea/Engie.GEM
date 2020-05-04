using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Exceptions
{
    /// <summary>
    /// Exception class for mapping errors between the API Models and Engie.Core Entities.
    /// </summary>
    internal class MappingException : Exception
    {
        public MappingException() { }

        public MappingException(string message) : base(message){ }

        public MappingException(string message, Exception inner) : base(message, inner) { }
    }
}