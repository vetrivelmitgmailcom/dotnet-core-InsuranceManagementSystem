using InsuranceManagementSystemMVC.BO;
using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Serilog;
using System.Diagnostics;
using NuGet.Protocol;
using Newtonsoft.Json;
using InsuranceManagementSystemMVC.Filters;

namespace InsuranceManagementSystemMVC.Controllers
{
    //[Route("[controller]")]
    //[Route("[controller]/[action]")]
    //[Route("Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        #region HomePage
        // POST: HomeController/Index
        [HttpGet]
        [Route("~/")]            //Global
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            try
            {
                Log.Information("Home page triggered");
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Home page is triggered");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion


        #region AboutPage
        // POST: HomeController/Index
        [HttpGet]
        [Route("About")]
        public IActionResult About()
        {
            try
            {
                string viewName = "About";
                string folderName = "Home";

                string viewPath = Path.Combine(_env.ContentRootPath, "Views", folderName, viewName + ".cshtml");


                Exception innerException = new Exception("About Page Not found");  //InnnerException(try)

                if (!System.IO.File.Exists(viewPath))
                {
                    throw new InsuranceManagementException("About Page Not Found,Please Contact Admin",innerException);
                }

                Log.Information("About page triggered");
                return View();
            }
            catch (InsuranceManagementException ex)
            {
                Log.Error(ex, "Error occured while About page is triggered");

                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion


        #region custom Error
        [HttpGet]
        [Route("Error")]
        public IActionResult ErrorPage()
        {
            ErrorViewModel? errorViewModel=new ErrorViewModel();
            var errorViewModelJsonString = HttpContext.Session.GetString("ErrorViewModel");
            if (errorViewModelJsonString != null)
            {
                errorViewModel = JsonConvert.DeserializeObject<ErrorViewModel>(errorViewModelJsonString);
            }


            Log.Error("Errorpage action triggered");


            if (errorViewModel != null)
            {
                //errorViewModel.ExceptionType = typeof(Exception).ToString();
                //errorViewModel.ExceptionType = ex.GetType().ToString();
                //errorViewModel.TargetSite = ex.TargetSite?.ToString() ?? "Unknown";
                //errorViewModel.ErrorMessage = ex.Message;
                //errorViewModel.InnerExceptionMessage = ex.InnerException?.Message;
                //errorViewModel.StackTrace = ex.StackTrace;
                //errorViewModel.Source = ex.Source;
                //errorViewModel.TargetSite = ex.TargetSite?.ToString();
                //errorViewModel.HelpLink = ex.HelpLink;
                //errorViewModel.HResult = ex.HResult;


                /////////////////////////////////////////////////////////////////////////////
                errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                errorViewModel.RequestUrl = HttpContext.Request.Path.ToString();
                errorViewModel.IsTransient = true;
                errorViewModel.ErrorTime = DateTime.UtcNow;
                errorViewModel.ServerName = Environment.MachineName;
                errorViewModel.ErrorPath = HttpContext.Request.Path;
                errorViewModel.HttpMethod = HttpContext.Request.Method;
                errorViewModel.RequestUrl = HttpContext.Request.Path;
                errorViewModel.ReferrerUrl = HttpContext.Request.Headers["Referer"];
                errorViewModel.UserAgent = HttpContext.Request.Headers["User-Agent"];
                errorViewModel.UserIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                Log.Error(errorViewModel.ToString(), "ErrorViewModel Message:");

            }
            Log.Error("Error page is display to the user");
            return View(errorViewModel);
        }
        #endregion



        #region default Error
        [Route("_Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            Log.Information("Error page triggered");
            string? message = "";
            string? routeWhereExceptionOccured = "";
            Exception exceptionThatOccured = new Exception();
            var exceptionFailure = HttpContext.Features.Get<IExceptionHandlerFeature>();    //It will be change(depending on frameswork)(remember that)

            if (exceptionFailure != null)
            {
                routeWhereExceptionOccured = exceptionFailure.Path;
                exceptionThatOccured = exceptionFailure.Error;

                message = routeWhereExceptionOccured + "----" + exceptionThatOccured.Message;
                _logger.Log(LogLevel.Error, message);                      //private readonly ILogger<HomeController> _logger
                Log.Error(exceptionFailure.ToString(), message);
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorPath = routeWhereExceptionOccured, ErrorMessage = exceptionThatOccured.Message });


            #region extra
            //// Set values for properties of the ErrorViewModel object
            //errorViewModel.RequestId = HttpContext.TraceIdentifier;
            //errorViewModel.ErrorPath = HttpContext.Request.Path;
            //errorViewModel.ErrorMessage = "An error occurred while processing your request.";
            //errorViewModel.StackTrace = "Sample stack trace.";
            //errorViewModel.InnerExceptionMessage = "Sample inner exception message.";
            //errorViewModel.ErrorTime = DateTime.UtcNow;
            //errorViewModel.ServerName = Environment.MachineName;
            //errorViewModel.AdditionalInfo = "Additional information about the error.";
            //errorViewModel.ExceptionType = "System.Exception";
            //errorViewModel.TargetSite = "SampleTargetSite";
            //errorViewModel.Data = new Dictionary<string, object>();
            //errorViewModel.HResult = 1234;
            //errorViewModel.Source = "SampleSource";
            //errorViewModel.LineNumber = 42;
            //errorViewModel.FileName = "SampleFileName.cs";
            //errorViewModel.HelpLink = "https://example.com/help";
            //errorViewModel.IsTransient = true;
            //errorViewModel.ErrorId = "123456";
            //errorViewModel.ErrorCategory = "SampleErrorCategory";
            //errorViewModel.ErrorSeverity = "SampleErrorSeverity";
            //errorViewModel.ErrorState = "SampleErrorState";
            //errorViewModel.ErrorNumber = 5678;
            //errorViewModel.HttpMethod = HttpContext.Request.Method;
            //errorViewModel.RequestUrl = HttpContext.Request.Path;
            //errorViewModel.ReferrerUrl = HttpContext.Request.Headers["Referer"];
            //errorViewModel.UserAgent = HttpContext.Request.Headers["User-Agent"];
            //errorViewModel.UserIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            //return View(errorViewModel);
            #endregion

        }

        #endregion


        #region ErrorViewModel
        public ErrorViewModel SetValuesToErrorViewModel(Exception ex)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.ExceptionType = ex.GetType().ToString();
            errorViewModel.TargetSite = ex.TargetSite?.ToString() ?? "Unknown";
            errorViewModel.ErrorMessage = ex.Message;
            errorViewModel.InnerExceptionMessage = ex.InnerException?.Message;
            errorViewModel.StackTrace = ex.StackTrace;
            errorViewModel.Source = ex.Source;
            errorViewModel.TargetSite = ex.TargetSite?.ToString();
            errorViewModel.HelpLink = ex.HelpLink;
            errorViewModel.HResult = ex.HResult;

            return errorViewModel;
        }
        #endregion
    }
}