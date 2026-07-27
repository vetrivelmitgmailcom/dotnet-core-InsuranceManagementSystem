using Microsoft.AspNetCore.Http;

namespace InsuranceManagementSystemMVC.Middleware
{
    public class CustomMiddleware3 : IMiddleware
    {
        private readonly ILogger _logger;

        public CustomMiddleware3(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("CustomMiddleware3");
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("Custom Middleware3 request");
            //await context.Response.WriteAsync("Custom Middleware3 request\n");

            await next(context);

            _logger.LogInformation("Custom Middleware3 response");
            //await context.Response.WriteAsync("Custom Middleware3 response\n");
        }
    }
}
