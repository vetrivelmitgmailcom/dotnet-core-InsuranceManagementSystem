using InsuranceManagementSystemMVC.BO;
using InsuranceManagementSystemMVC.Models;
using InsuranceManagementSystemMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace InsuranceManagementSystemMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly InsuranceBO _insuranceBO;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<LoginController> _logger;

        #region Constructor
        public LoginController(ILogger<LoginController> logger, IHttpContextAccessor httpContextAccessor, ILoginRepository loginRepository, ICustomerRepository customerRepository, IPolicyRepository policyRepository, INomineeRepository nomineeRepository, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _insuranceBO = new InsuranceBO(loginRepository, customerRepository, policyRepository, nomineeRepository, paymentRepository);
        }
        #endregion Constructor


        #region Login
        // GET: LoginController/Login
        [HttpGet]
        [Route("Login")]
        public IActionResult Index()
        {
            try
            {
                Log.Information("Login Page triggered");
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while logging in");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }

        // POST: LoginController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public IActionResult Index(Admin admin)
        {
            try
            {
                Log.Information("Login form is successfully submitted {0}", admin);
                int adminId = _insuranceBO.FLogin(admin);


                if (adminId > 0)
                {
                    Log.Information("Login successfull [adminId = {0}]", adminId);
                    TempData["Title"] = "Login Successfull!";
                    TempData["SuccessMessage"] = "You are Welcome";
                    _httpContextAccessor.HttpContext?.Session.SetString("AdminId", adminId + "");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Log.Information("Login failed [adminId = {0}]", adminId);
                    TempData["Title"] = "Login failed!";
                    TempData["FailureMessage"] = "Email or Password is incorrect";
                    Log.Information("Login is failed [adminId={0}]", adminId);
                    return View();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while logging in {0}", admin);
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion Login

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            try
            {
                _httpContextAccessor.HttpContext?.Session.Remove("AdminId");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
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
