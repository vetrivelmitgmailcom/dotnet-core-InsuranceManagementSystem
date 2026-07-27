using InsuranceManagementSystemMVC.BO;
using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using InsuranceManagementSystemMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace InsuranceManagementSystemMVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly InsuranceBO _insuranceBO;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<PaymentController> _logger;

        #region Constructor
        public PaymentController(ILogger<PaymentController> logger, IHttpContextAccessor httpContextAccessor, ILoginRepository loginRepository, ICustomerRepository customerRepository, IPolicyRepository policyRepository, INomineeRepository nomineeRepository, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _insuranceBO = new InsuranceBO(loginRepository, customerRepository, policyRepository, nomineeRepository, paymentRepository);
        }
        #endregion Constructor


        #region AddPayments
        //GET: PaymentController/PaymentsInfo
        [HttpGet]
        [Route("Policy/Payment")]
        public IActionResult AddPayment()
        {
            try
            {
                Log.Information("Add Payment Page triggered");
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Adding Payment Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }


        //Post: PaymentController/PaymentsInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Policy/Payment")]
        public IActionResult AddPayment(Payment newPayment)
        {
            try
            {
                Log.Information("Add Payment Page triggered");
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Adding Payment Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion AddPayments



        #region PaymentsInfo
        //GET: NomineeController/PaymentsInfo
        [HttpGet]
        [Route("Policy/Payments/All")]
        public IActionResult PaymentsInfo()
        {
            try
            {
                Log.Information("Payment Information Page triggered");
                var payments = _insuranceBO.AllPaymentInformation();


                if (payments.Any())
                {
                    Log.Information("Retrieving Payment details successfull [count={0}]\n{1}", payments.Count(),payments);
                    return View(payments);
                }
                else
                {
                    Log.Information("Retrieving Payment details failed [count={0}]\n{1}", payments.Count(), payments);
                    throw new RecordFetchingFailedException("Failed to fetch the Record,Please contact Admin");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Payment Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion PaymentInfo


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
