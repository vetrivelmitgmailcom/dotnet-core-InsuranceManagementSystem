using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InsuranceManagementSystemMVC.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware2
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomMiddleware2(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger("CustomMiddleware2");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Custom Middleware2 request");
            await _next(httpContext);
            _logger.LogInformation("Custom Middleware2 response");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddleware2Extensions
    {
        public static IApplicationBuilder UseCustomMiddleware2(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware2>();
        }
    }
}
