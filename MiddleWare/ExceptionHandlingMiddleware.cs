using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace CMS.MiddleWare
{
    public class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) {  _logger = logger; }
        public async Task Invoke(FunctionContext functionContext, FunctionExecutionDelegate executionDelegate)
        {
            try
            {
                await executionDelegate(functionContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured");
                throw;
            }
        }
    }
}
