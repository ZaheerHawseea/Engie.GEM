using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Exceptions
{
    /// <summary>
    /// Models the error details sent to the client as part of the API response.
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Overriden ToString() to provide the message in JSON format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{ Message: \"{Message}\" }}";
        }
    }
}
