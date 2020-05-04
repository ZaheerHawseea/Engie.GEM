using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.GEM.ProductionPlan.API.Exceptions
{
    /// <summary>
    /// Middleware used to handle and log exceptions.
    /// </summary>
    public class EngieExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<EngieExceptionMiddleware> logger;

        public EngieExceptionMiddleware(RequestDelegate next, ILogger<EngieExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                LogException(context, ex);

                context.Response.StatusCode = 500;

                // Only write the message, no stack trace should reach the client
                await context.Response.WriteAsync(new ErrorDetail() { Message = ex.Message }.ToString());
            }
        }

        private void LogException(HttpContext context, Exception ex)
        {
            logger.LogError($"Message: {ex.Message} \nStackTrace: {ex.StackTrace}");
        }
    }
}
