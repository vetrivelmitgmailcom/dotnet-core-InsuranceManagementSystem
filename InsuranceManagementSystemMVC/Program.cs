using CommonUtility;
using InsuranceManagementSystemMVC.Models;
using InsuranceManagementSystemMVC.Repository;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Http;
using InsuranceManagementSystemMVC.Filters;
using Microsoft.AspNetCore.Builder;
using InsuranceManagementSystemMVC.Middleware;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.ComponentModel;

namespace InsuranceManagementSystemMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Serilog
            InsuranceManagementSystemLogger logger = new InsuranceManagementSystemLogger();
            logger.BuildConfigure();
            Log.Information("Insurance Management System Application Started...");
            #endregion

            #region log
                                                                                                                                                            //build pannurathukku munnadi logger add pannanum
            #region Logger                      
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();                                                                                                                                 //ithu console la print pannum
            builder.Logging.AddDebug();                                                                                                             //ithu debug la print agum
          //builder.Logging.AddEventLog();
            #endregion


            #region logging(Method2)Enable Logging Inside the program.cs using LoggerFactory

            //var loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    builder.AddConsole();
            //    builder.AddDebug();
            //});

            //var _logger = loggerFactory.CreateLogger<Program>();
            //_logger.LogInformation("Enable(using LoggerFactory) Logging inside the Program.cs");
            #endregion

            #endregion



            //// Add services to the container.
            #region Context
            var conString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<InsuranceContext>(option => option.UseSqlServer(conString));
            #endregion

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<CustomMiddleware3>();


            #region Add Filter
            //builder.Services.AddControllersWithViews();   //Default(It will be change when Add AdminAuthenticationFilter globally)
            //builder.Services.AddScoped<AdminAuthenticationFilter>(); //Add Filter to specific controller or action -->     //[ServiceFilter(typeof(AdminAuthenticationFilter))] -->  Add this line at the top of the controller or action


            builder.Services.AddControllersWithViews(options =>              //Add AdminAuthenticationFilter Globally
            {
                options.Filters.Add(typeof(AdminAuthenticationFilter));
            });
            #endregion


            #region session
            builder.Services.AddSession();

            //;
            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromSeconds(1200);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});
            #endregion



            #region set Repository
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<INomineeRepository, NomineeRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            #endregion


            var app = builder.Build();

            #region logging(Method1)Enable Logging Inside the program.cs
            var _logger = app.Services.GetRequiredService<ILogger<Program>>();
            _logger.LogInformation("Enable Logging inside the Program.cs");
            #endregion


            #region Middlewar

            #region Inline Middleware
            app.Use(async (context, next) =>
            {
                _logger.LogInformation("Inline Middleware request");
                //await context.Response.WriteAsync("Inline Middeware request\n");

                await next();

                _logger.LogInformation("Inline Middleware response");
                //await context.Response.WriteAsync("Inline Middeware response\n");
            });
            #endregion
          
            #region CustomMiddlewar
            app.UseCustomMiddleware1();
            app.UseCustomMiddleware2();
            app.UseMiddleware<CustomMiddleware3>();
            #endregion

            //app.Map("/vetri",Customcode);
            #endregion


            #region Exception
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseMiddleware<ExceptionMiddleware>()
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }
            #endregion

            app.UseHttpsRedirection();                                                                                                   //ithu session la erukkurathukku nu nenaikkuren
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            #region attribute based routing
            //app.MapControllers();                                                     //ipdi panna only attribut routing mattum tha panna mudium,but keela erukura mathiri panna attribute and conventional routing panalam

            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");    //app.MapControllers(); kku pathila conventional rounting kanatha{app.MapControllerRoute(name: "",pattern: "");} use panna customer controller la         [Route("/Customer/GetStatesByCountry/{id?}")  intha mathirilam set panna theva illai,ipdi(MapControllerRoute) use panna attribute and conventional routing rendaium same project la use pannikalam.


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            #endregion

            #region conventional based routing
            //app.MapControllerRoute(                                         //Default-->note that this method is used in ASP.NET Core 2.x versions and is different from the approach used in ASP.NET Core 3.0 and later versions, which involves using the UseEndpoints middleware to define routes.
            //           name: "default",
            //           pattern: "{controller=Home}/{action=Index}/{id?}");


            //app.UseEndpoints(endpoints => {
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
            #endregion


            app.Run();
        }
    }
}