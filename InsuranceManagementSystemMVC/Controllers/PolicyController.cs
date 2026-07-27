using InsuranceManagementSystemMVC.BO;
using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using InsuranceManagementSystemMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Globalization;

namespace InsuranceManagementSystemMVC.Controllers
{
    public class PolicyController : Controller
    {
        private readonly InsuranceBO _insuranceBO;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<PolicyController> _logger;


        #region Constructor
        public PolicyController(ILogger<PolicyController> logger, IHttpContextAccessor httpContextAccessor, ILoginRepository loginRepository,ICustomerRepository customerRepository, IPolicyRepository policyRepository, INomineeRepository nomineeRepository, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _insuranceBO = new InsuranceBO(loginRepository, customerRepository, policyRepository, nomineeRepository, paymentRepository);
        }
        #endregion Constructor



        #region AddPolicy
        // GET: PolicyController/AddPolicy
        [HttpGet]
        [Route("Policy/AddPolicy/{id?}")]
        public IActionResult AddPolicy(long customerId)
        {
            try
            {
                Log.Information("Add Policy Page triggered");
                Log.Information("Add Policy to the Customer...[customerId={0}]", customerId);
                var customer = _insuranceBO.GetCustomerById(customerId);
                if (customer.StatusId == 0)
                {
                    return NotFound();
                }
                else
                {
                    if (customer != null)
                    {
                        Log.Information("Retrieving customer details successfull {0}", customer);
                        var customerName = customer.FirstName + " " + customer.LastName;

                        //string? message = _httpContextAccessor.HttpContext?.Session.GetString("ViewTo")
                        //Console.WriteLine(message)
                        ViewBag.CustomerId = customerId;
                        ViewBag.CustomerName = customerName;
                        ViewBag.InsuranceTypeList = _insuranceBO.FGetInsuranceType();
                        ViewBag.ModeOfPremiumList = _insuranceBO.FGetModeOfPremium();
                        ViewBag.PaymentTypeList = _insuranceBO.FGetPaymentType();
                        ViewBag.RelationshipList = _insuranceBO.FGetRelationship();
                        Log.Information("Add Policy form is opened to fill the policy details");
                        return View();
                    }
                    else
                    {
                        Log.Information("Retrieving customer details failed {0}", customer);
                        throw new RecordFetchingFailedException("Failed to fetch the Customer Record for Add policy");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Adding Policy to the Customer");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }



        // POST: PolicyController/AddPolicy
        //-->POST: PolicyController/AdditionalPolicy
        #endregion AddPolicy




        #region AdditionalPolicy
        // POST: PolicyController/AdditionalPolicy
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Policy/AddPolicy")]
        public IActionResult AddPolicy(PolicyDetail newPolicy, NomineeDetail newNominee, Payment payment)
        {
            try
            {
                newPolicy.NomineeDetails = new List<NomineeDetail> { newNominee };
                newPolicy.PolicyValue.Payments = new List<Payment> { payment };
                Log.Information("Add Policy form is successfully submitted {0}", newPolicy);

                long policyId = _insuranceBO.AddPolicy(newPolicy);

                if (policyId > 0)
                {
                    Log.Information("Add Policy to the Customer Successfull [policyId={0}]", policyId);
                    string? message = _httpContextAccessor.HttpContext?.Session.GetString("ViewTo");


                    TempData["Title"] = "Policy Added Successfully!";
                    TempData["SuccessMessage"] = "You have successfully added a customer policy";
                    switch (message)
                    {
                        case "RegisterCustomer":
                            return RedirectToAction("Index", "Home");
                        case "CustomerInfo":
                            return RedirectToAction("CustomerInfo", "Customer");
                        case "GetCustomerInfo":
                            return RedirectToAction("GetCustomerInfo", "Customer");
                        case "GetCustomerInfoMore":
                            return RedirectToAction("GetCustomerInfoMore", "Customer");
                        default:
                            return RedirectToAction("CustomerInfo", "Customer");
                    }
                }
                else
                {
                    Log.Information("Add Policy to the Customer failed [policyId={0}]", policyId);
                    throw new RecordInsertionFailedException("Failed to insert(update) the Record.Please Contact Admin");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Adding Policy to the Customer", newPolicy);
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion AdditionalPolicy




        #region PolicyInfo,GetPolicyInfo.PolicyInfoMore

        // Get: PolicyController/PolicyInfo
        [HttpGet]
        [Route("Policy/All")]
        public IActionResult PolicyInfo()
        {
            try
            {
                Log.Information("Policy Information Page triggered");
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Policy Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }



        // Get: PolicyController/GetPolicyInfo
        [HttpGet]
        [Route("Policy/GetPolicy")]
        public IActionResult GetPolicyInfo()
        {

            try
            {
                Log.Information("Get Policy Information Page triggered");
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Policy Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }

        }



        // Get: PolicyController/PolicyInfoMore
        [HttpGet]
        [Route("Policy/Information/{id?}")]
        public IActionResult PolicyInfoMore(long policyId)
        {
            try
            {
                Log.Information("Policy Information Page triggered [policyId={0}]", policyId);
                var policy = _insuranceBO.AdditionalPolicyInformation(policyId);
                if (policy.StatusId == 0)
                {
                    return NotFound();
                }
                else
                {
                    if (policy != null)
                    {
                        Log.Information("Retrieving policy details successfull {0}", policy);
                        return View(policy);
                    }
                    else
                    {
                        Log.Information("Retrieving policy details failed {0}", policy);
                        throw new RecordFetchingFailedException("Failed to fetch the Record,Please contact Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching policy Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion PolicyInfo,GetPolicyInfo.PolicyInfoMore




        #region PartialView {PolicyInfo:PolicyInfoPartial}  {GetPolicyInfo:GetPolicyInfoPartial}
        // Get: PolicyController/PolicyInfoPartial
        [HttpGet]
        public IActionResult PolicyInfoPartial()
        {
            try
            {
                Log.Information("Policy Information Partial View triggered");

                var policy = _insuranceBO.FPolicyInfoPartial();

                if (policy.Any())
                {
                    Log.Information("Retrieving policy details successfull [count={0}]\n{1}", policy.Count(),policy);
                    return PartialView("_GetPolicyInfoPartial", policy);
                }
                else
                {
                    Log.Information("Retrieving policy details failed [count={0}]\n{1}", policy.Count(), policy);
                    throw new RecordFetchingFailedException("Failed to fetch the Record,Please contact Admin");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Policy Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }



        // GET: PolicyController/GetPolicyInfoPartial
        [HttpGet]
        public IActionResult GetPolicyInfoPartial(string DateOfIssue, string AmountOfPeriod)
        {
            DateTime dateOfIssue;
            int amountOfPeriod;
            try
            {
                Log.Information("GetPolicyInformation Partial View triggered [DateOfIssue={0},AmountOfPeriod={1}]", DateOfIssue, AmountOfPeriod);
                if (DateTime.TryParseExact(DateOfIssue, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfIssue) && int.TryParse(AmountOfPeriod, out amountOfPeriod))
                {
                    var policy = _insuranceBO.FGetPolicyInfoPartial(dateOfIssue, amountOfPeriod);

                    if (policy.Any())
                    {
                        Log.Information("Retrieving policy details successfull {0}", policy);
                        return PartialView("_GetPolicyInfoPartial", policy);
                    }
                    else
                    {
                        Log.Information("Retrieving policy details failed {0}", policy);
                        Log.Information("Empty View is Send to the GetPolicyInformation Page");
                        return PartialView("_EmptyPartial");
                    }
                }
                else
                {
                    throw new InsuranceManagementException("Failed to parse DateOfIssue as DateTime and AmountOfPeriod as Integer");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Policy Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion PartialView



        #region Procedure
        // GET: PolicyController/CalculatePremiumAmount
        [HttpGet]
        public JsonResult CalculatePremiumAmount(string CustomerId, string AmountOfPeriod, string InsuredDeclaredValue)
        {
            long customerId;
            int amountOfPeriod;
            double insuredDeclaredValue;
            try
            {
                Log.Information("CalculatePremiumAmount method triggered [CustomerId={0},AmountOfPeriod={1},InsuredDeclaredValue={2}]", CustomerId, AmountOfPeriod, InsuredDeclaredValue);
                if (long.TryParse(CustomerId, out customerId) && int.TryParse(AmountOfPeriod, out amountOfPeriod) && double.TryParse(InsuredDeclaredValue, out insuredDeclaredValue))
                {
                    double _PremiumAmount = _insuranceBO.FCalculatePremiumAmount(customerId, amountOfPeriod, insuredDeclaredValue);

                    if (_PremiumAmount > 0)
                    {
                        Log.Information("CalculatePremiumAmount method is return {0}", _PremiumAmount);
                        return Json(_PremiumAmount);
                    }
                    else
                    {
                        Log.Information("CalculatePremiumAmount method is return {0}", _PremiumAmount);
                        return Json(0);
                    }

                }
                else
                {
                    throw new InsuranceManagementException("Failed to parse customerId as long integer and AmountOfPeriod as Integer and InsuredDeclaredValue as Double");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw new InsuranceManagementException(ex.Message, ex);
            }
        }
        #endregion Procedure


        #region
        // GET: PolicyController/CustomerInfoMore
        [HttpGet]
        public IActionResult CustomerInfoMore()
        {
            try
            {
                var customerId = _httpContextAccessor.HttpContext?.Session.GetString("customerId");
                _httpContextAccessor.HttpContext?.Session.Remove("customerId");
                return RedirectToAction("CustomerInfoMore", "Customer", new { customerId = customerId });
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw new InsuranceManagementException(ex.Message, ex);
            }
        }
        #endregion


        #region DeletePolicy
        // GET: PolicyController/PolicyCustomer
        [HttpGet]
        [Route("Policy/Delete/{id?}")]
        public IActionResult DeletePolicy(long policyId, string viewFrom)
            {
            try
            {
                Log.Information("Delete policy details for the customer...[policyId={0}]", policyId);
                var flag = _insuranceBO.DeletePolicy(policyId);
                if (flag)
                {
                    Log.Information("Deleting policy details successfull{0}", flag);
                    Log.Information("session:::{0}", viewFrom);

                    //TempData["Title"] = "Successfully Updated!";
                    //TempData["SuccessMessage"] = "You have successfully updated the customer details";
                    switch (viewFrom)
                    {
                        case "PolicyInfo":
                            return RedirectToAction("All", "Policy");
                        default:
                            return RedirectToAction("All", "Policy");
                    }
                }
                else
                {
                    Log.Information("Deleting policy details failed {0}", flag);
                    throw new InsuranceManagementException("Failed to delete the policy details");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while deleting Customer Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion DeletePolicy


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
