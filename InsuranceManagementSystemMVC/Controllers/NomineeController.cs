using InsuranceManagementSystemMVC.BO;
using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using InsuranceManagementSystemMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;


namespace InsuranceManagementSystemMVC.Controllers
{
    [Route("Policy/Nominee")]
    public class NomineeController : Controller
    {

        private readonly InsuranceBO _insuranceBO;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<NomineeController> _logger;

        #region Constructor
        public NomineeController(ILogger<NomineeController> logger, IHttpContextAccessor httpContextAccessor, ILoginRepository loginRepository, ICustomerRepository customerRepository, IPolicyRepository policyRepository, INomineeRepository nomineeRepository, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _insuranceBO = new InsuranceBO(loginRepository, customerRepository, policyRepository, nomineeRepository, paymentRepository);
        }
        #endregion Constructor


        #region NomineeInfo
        //GET: NomineeController/NomineeInfo
        [HttpGet]
        [Route("All")]
        public IActionResult NomineeInfo()
        {
            try
            {
                Log.Information("Nominee Information Page triggered");
                var nominees = _insuranceBO.AllNomineeInformation();


                if (nominees.Any())
                {
                    Log.Information("Retrieving Nominee details successfull [count={0}]\n{1}", nominees.Count(),nominees);
                    return View(nominees);
                }
                else
                {
                    Log.Information("Retrieving nominess details failed [count={0}]\n{1}", nominees.Count(), nominees);
                    throw new RecordFetchingFailedException("Failed to fetch the Record,Please contact Admin");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Nominee Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion NomineeInfo


        #region validation
        [HttpGet]
        [Route("CheckMobileNumberExist/{id?}")]
        public JsonResult CheckMobileNumberExist(string mobileNumber)
        {
            long mobileNumberLong;
            bool mobileExistOrNot;
            try
            {
                Log.Information("CheckMobileNumberExist method is triggered [mobileNumber={0}]", mobileNumber);
                if (long.TryParse(mobileNumber, out mobileNumberLong))
                {
                    mobileExistOrNot = _insuranceBO.FCheckMobileNumberExist_Nominee(mobileNumberLong);
                }
                else
                {
                    mobileExistOrNot = false;
                }
                Log.Information("CheckMobileNumberExist method is return {0}", mobileExistOrNot);
                return Json(mobileExistOrNot);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw new InsuranceManagementException(ex.Message, ex);
            }
        }


        [HttpGet]
        [Route("CheckAadharNumberExist/{id?}")]
        public JsonResult CheckAadharNumberExist(string aadharNumber)
        {
            try
            {
                Log.Information("CheckAadharNumberExist method is triggered [aadharNumber={0}]", aadharNumber);

                bool aadharNumberExistOrNot = _insuranceBO.FCheckAadharNumberExist_Nominee(aadharNumber);
                Log.Information("CheckAadharNumberExist method is return {0}", aadharNumberExistOrNot);
                return Json(aadharNumberExistOrNot);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw new InsuranceManagementException(ex.Message, ex);
            }
        }


        [HttpGet]
        [Route("CheckPANExist/{id?}")]
        public JsonResult CheckPANExist(string panNumber)
        {
            try
            {
                Log.Information("CheckPANExist method is triggered [panNumber={0}]", panNumber);
                bool panExistOrNot = _insuranceBO.FCheckPANExist_Nominee(panNumber);
                Log.Information("CheckPANExist method is return {0}", panExistOrNot);
                return Json(panExistOrNot);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw new InsuranceManagementException(ex.Message, ex);
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
