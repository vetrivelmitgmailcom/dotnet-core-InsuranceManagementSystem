using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InsuranceManagementSystemMVC.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware1
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomMiddleware1(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger("CustomMiddleware1");
        }

        public async Task Invoke(HttpContext httpContext)
        {

            _logger.LogInformation("Custom Middleware1 request");
            await _next(httpContext);
            _logger.LogInformation("Custom Middleware1 response");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddleware1Extensions
    {
        public static IApplicationBuilder UseCustomMiddleware1(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware1>();
        }
    }
}
