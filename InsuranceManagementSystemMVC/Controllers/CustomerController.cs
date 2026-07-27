using InsuranceManagementSystemMVC.BO;
using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using InsuranceManagementSystemMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Http;
using InsuranceManagementSystemMVC.Filters;

namespace InsuranceManagementSystemMVC.Controllers
{

    //[ServiceFilter(typeof(AdminAuthenticationFilter))]  //Add Filter to specific controller or action
    public class CustomerController : Controller
    {
        private readonly InsuranceBO _insuranceBO;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CustomerController> _logger;


        #region Constructor
        public CustomerController(ILogger<CustomerController> logger, IHttpContextAccessor httpContextAccessor, ILoginRepository loginRepository, ICustomerRepository customerRepository, IPolicyRepository policyRepository, INomineeRepository nomineeRepository, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _insuranceBO = new InsuranceBO(loginRepository, customerRepository, policyRepository, nomineeRepository, paymentRepository);
            //CheckAuthenticationAndRedirect()
        }
        #endregion Constructor


        #region Authentication
        //private void CheckAuthenticationAndRedirect()
        //{
        //    var adminId = _httpContextAccessor.HttpContext?.Session.GetString("AdminId");
        //    if (adminId == null)
        //    {
        //        _httpContextAccessor.HttpContext?.Response.Redirect("/");   //"/Home/Index"
        //    }
        //}
        #endregion


        #region RegisterCustomer
        // GET: CustomerController/RegisterCustomer
        [HttpGet]
        [Route("Customer/Register")]
        public IActionResult RegisterCustomer()
        {
            try
            {
                _logger.Log(LogLevel.Information, "Customer Registration Page triggered");                                //It will print in console
                Log.Information("Customer Registration Page triggered");
                Log.Information("Registration of Customer Details...");
                ViewBag.CountryList = GetCountries();
                Log.Information("Customer Registration form is opened to fill the personal details");
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Registering Customer Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }

        // POST: CustomerController/RegisterCustomer
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Customer/Register")]
        public IActionResult RegisterCustomer(Customer newCustomer, PersonalDetail personalDetail)
        {
            try
            {
                newCustomer.PersonalDetails = new List<PersonalDetail> { personalDetail };
                Log.Information("Customer Registration form is successfully submitted {0}", newCustomer);
                long customerId = _insuranceBO.RegisterCustomer(newCustomer);


                if (customerId > 0)
                {
                    Log.Information("Customer Registration Successfull [customerId = {0}]", customerId);
                    TempData["Title"] = "Registration Successfull!";
                    TempData["SuccessMessage"] = "You have successfully registered the customer";
                    TempData["FooterMessage"] = "Click Continue to add the policy for the customer";
                    return RedirectToAction("AddPolicy", "Policy", new { customerId = customerId });
                }
                else
                {
                    Log.Information("Customer Registration failed [customerId={0}]", customerId);
                    throw new RecordInsertionFailedException("Failed to insert the Record");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Registering Customer details {0}", newCustomer);
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion RegisterCustomer



        #region UpdateCustomer
        // GET: CustomerController/UpdateCustomer
        [HttpGet]
        [Route("Customer/Update/{id?}")]
        public IActionResult UpdateCustomer(long customerId)
        {
            try
            {
                Log.Information("Update Customer Page triggered");
                Log.Information("Update details for Customer...[CsutomerId={0}]", customerId);
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
                        ViewBag.MobileNumber = customer.PersonalDetails.First().MobileNumber;
                        ViewBag.PanNumber = customer.PersonalDetails.First().PanNumber;
                        ViewBag.AadharNumber = customer.PersonalDetails.First().AadharNumber;
                        ViewBag.Email = customer.PersonalDetails.First().Email;

                        ViewBag.CountryList = GetCountries();
                        ViewBag.StateId = customer.PersonalDetails.First().StateId;
                        ViewBag.CityId = customer.PersonalDetails.First().CityId;
                        Log.Information("Update Customer form is opened to update the personal details");
                        return View(customer);
                    }
                    else
                    {
                        Log.Information("Retrieving customer details failed {0}", customer);
                        throw new RecordFetchingFailedException("Failed to fetch the Record for Update Customer");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Updating Customer Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }



        // POST: CustomerController/UpdateCustomer
        [HttpPost]
        [ValidateAntiForgeryToken]                //Note: The hidden input field should have the name __RequestVerificationToken as it is the default name used by ASP.NET Core for the anti-forgery token.That's how you can use the relative Tag Helper syntax to achieve the equivalent functionality of @Html.AntiForgeryToken() in ASP.NET Core.[    <input type="hidden" asp-for="@Request.Form["__RequestVerificationToken"]" />]
        [Route("Customer/Update/{id?}")]
        public IActionResult UpdateCustomer(long CustomerId, Customer existCustomer, PersonalDetail personalDetail)
        {
            try
            {
                existCustomer.PersonalDetails = new List<PersonalDetail> { personalDetail };
                Log.Information("Update Customer form is successfully submitted {0}", existCustomer);
                long customerId = _insuranceBO.UpdateCustomer(CustomerId, existCustomer);

                if (customerId > 0)
                {
                    Log.Information("Customer details Updated Successfull [customerId={0}]", customerId);
                    string? message = _httpContextAccessor.HttpContext?.Session.GetString("ViewTo");

                    TempData["Title"] = "Successfully Updated!";
                    TempData["SuccessMessage"] = "You have successfully updated the customer details";
                    switch (message)
                    {
                        case "CustomerInfo":
                            return RedirectToAction("CustomerInfo", "Customer");
                        case "GetCustomerInfo":
                            return RedirectToAction("GetCustomerInfo", "Customer");
                        default:
                            return RedirectToAction("CustomerInfo", "Customer");
                    }
                }
                else
                {
                    Log.Information("Customer details updated failed [customerId={0}]", customerId);
                    throw new RecordInsertionFailedException("Failed to update the Record");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Updating Customer details", existCustomer);
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        #endregion UpdateCustomer




        #region CustomerInfo,GetCustomerInfo,CustomerInfoMore
        // GET: CustomerController/CustomerInfo
        [HttpGet]
        [Route("Customer/All")]
        public IActionResult CustomerInfo()
        {
            try
            {
                Log.Information("Customers Information Page triggered");
                var customers = _insuranceBO.AllCustomerInformation();

                if (customers.Any())
                {
                    Log.Information("Retrieving customers details successfull [count={0}]\n{1}", customers.Count(),customers);
                    return View(customers);
                }
                else
                {
                    Log.Information("Retrieving customers details failed [count={0}]\n{1}", customers.Count(), customers);
                    throw new RecordFetchingFailedException("Failed to fetch the Record,Please contact Admin");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Customers Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }


        // GET: CustomerController/GetCustomerInfo
        [HttpGet]
        [Route("Customer/GetCustomer")]
        public IActionResult GetCustomerInfo()
        {
            try
            {
                Log.Information("Get Customers Information Page triggered");
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Customers Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }
        }



        //GET:
        //-->GetCustomerInfo:GetCustomerInfoPartial



        // GET: CustomerController/CustomerInfoMore
        [HttpGet]
        [Route("Customer/Information/{id?}")]
        public IActionResult CustomerInfoMore(long customerId)
        {
            try
            {
                Log.Information("Customer Information Page triggered [customerId={0}]", customerId);
                var customer = _insuranceBO.AdditionalCustomerInformation(customerId);

                if (customer.StatusId == 0)
                {
                    return NotFound();
                }
                else 
                {     
                    if (customer != null)
                    {
                        Log.Information("Retrieving customer details successfull {0}", customer);
                        return View(customer);
                    }
                    else
                    {
                        Log.Information("Retrieving customer details failed {0}", customer);
                        throw new RecordFetchingFailedException("Failed to fetch the Record,Please contact Admin");
                    }
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Customer Details");
                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }

        }
        #endregion  CustomerInfo,GetCustomerInfo,CustomerInfoMore




        #region PartialView {GetCustomerInfo:GetCustomerInfoPartial}
        // GET: CustomerController/GetCustomerInfoPartial
        [HttpGet]
        //[Route("/Customer/GetCustomerInfoPartial/{id?}")]    
        public IActionResult GetCustomerInfoPartial(string customerId)
        {
            long CustomerId;

            try
            {
                Log.Information("GetCustomerInformation Partial View triggered [customerId={0}]", customerId);
                if (long.TryParse(customerId, out CustomerId))
                {

                    var customer = _insuranceBO.FGetCustomerInfoPartial(CustomerId);
                    if (customer.Any())
                    {
                        Log.Information("Retrieving customer details successfull {0}", customer);
                        return PartialView("_GetCustomerInfoPartial", customer);
                    }
                    else
                    {
                        Log.Information("Retrieving customer details failed {0}", customer);
                        Log.Information("Empty View is Send to the GetCustomerInformation Page");
                        return PartialView("_EmptyPartial");
                    }
                }
                else
                {
                    throw new InsuranceManagementException("Failed to parse customerId as long integer");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while Fetching Customer Details");

                #region setValuesToErrorViewModel
                var errorViewModelJsonString = JsonConvert.SerializeObject(SetValuesToErrorViewModel(ex));
                HttpContext.Session.SetString("ErrorViewModel", errorViewModelJsonString);
                #endregion
                return RedirectToAction("ErrorPage", "Home");
            }

        }
        #endregion PartialView



        #region DeleteCustomer
        // GET: CustomerController/DeleteCustomer
        [HttpGet]
        [Route("Customer/Delete/{id?}")]
        public IActionResult DeleteCustomer(long customerId, string viewFrom)
        {
            try
            {
                Log.Information("Delete details for Customer...[CsutomerId={0}]", customerId);
                var flag = _insuranceBO.DeleteCustomer(customerId);
                if (flag)
                {
                    Log.Information("Deleting customer details successfull{0}", flag);
                    Log.Information("session:::{0}", viewFrom);

                    //TempData["Title"] = "Successfully Updated!";
                    //TempData["SuccessMessage"] = "You have successfully updated the customer details";
                    switch (viewFrom)
                    {
                        case "CustomerInfo":
                            return RedirectToAction("All", "Customer");
                        case "GetCustomerInfo":
                            return RedirectToAction("GetCustomer", "Customer");
                        default:
                            return RedirectToAction("All", "Customer");
                    }

                }
                else
                {
                    Log.Information("Deleting customer details failed {0}", flag);
                    throw new InsuranceManagementException("Failed to delete the Customer Records,Please Contact Admin");
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
        #endregion DeleteCustomer


        #region Select Box Data

        public List<SelectListItem>? GetCountries()
        {

            try
            {
                Log.Information("GetCountries method is triggered");

                List<SelectListItem> countryList = _insuranceBO.FGetCountries();
                if (countryList.Any())
                {
                    Log.Information("Fetching Country list successfull [count={0}]\n{1}", countryList.Count,countryList);
                    return countryList;
                }
                else
                {
                    Log.Information("Fetching Country list failed [count={0}]\n{1}", countryList.Count,countryList);
                    throw new CountryNotFoundException("Empty Country List Found");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw new InsuranceManagementException(ex.Message, ex);
            }
        }



        [HttpGet]
        //[Route("/Customer/GetStatesByCountry/{id?}")    //program.cs la  {app.MapControllerRoute(name: "",pattern: "");-->conventional routing kanathu} ipdi podama {app.MapControllers();-->attribute routing kanathu}ipdi potta inga ipdi set pannaum
        public JsonResult GetStatesByCountry(int countryId)
        {
            try
            {
                Log.Information("GetStatesByCountry method is triggered");

                List<SelectListItem> states = _insuranceBO.FGetStates(countryId);
                if (states.Any())
                {
                    Log.Information("Fetching State list successfull [count={0}]\n{1}", states.Count, states);
                    return Json(states);
                }
                else
                {
                    Log.Information("Fetching State list failed [count={0}]\n{1}", states.Count, states);
                    throw new StateNotFoundException("Empty State List Found");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw new InsuranceManagementException(ex.Message, ex);
            }

        }



        [HttpGet]
        [Route("/Customer/GetCitiesByState/{id?}")]
        public JsonResult GetCitiesByState(int stateId)
        {
            try
            {
                Log.Information("GetCitiesByState method is triggered");
                List<SelectListItem> cities = _insuranceBO.FGetCities(stateId);
                if (cities.Any())
                {
                    Log.Information("Fetching City list Successfull [count={0}]\n{1}", cities.Count,cities);
                    return Json(cities);
                }
                else
                {
                    Log.Information("Fetching City list failed [count={0}]\n{1}", cities.Count, cities);
                    throw new CityNotFoundException("Empty City List Found");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw new InsuranceManagementException(ex.Message, ex);
            }
        }
        #endregion Select Box Data


        #region validation
        [HttpGet]
        public JsonResult CheckMobileNumberExist(string mobileNumber)
        {

            long mobileNumberLong;
            bool mobileExistOrNot;
            try
            {
                Log.Information("CheckMobileNumberExist method is triggered [mobileNumber={0}]", mobileNumber);
                if (long.TryParse(mobileNumber, out mobileNumberLong))
                {
                    mobileExistOrNot = _insuranceBO.FCheckMobileNumberExist(mobileNumberLong);
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
        public JsonResult CheckEmailExist(string email)
        {
            try
            {
                Log.Information("CheckEmailExist method is triggered [email={0}]", email);
                bool emailExistOrNot = _insuranceBO.FCheckEmailExist(email);
                Log.Information("CheckEmailExist method is return {0}", emailExistOrNot);
                return Json(emailExistOrNot);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw new InsuranceManagementException(ex.Message, ex);
            }
        }



        [HttpGet]
        public JsonResult CheckAadharNumberExist(string aadharNumber)
        {
            try
            {
                Log.Information("CheckAadharNumberExist method is triggered [aadharNumber={0}]", aadharNumber);
                bool aadharNumberExistOrNot = _insuranceBO.FCheckAadharNumberExist(aadharNumber);
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
        public JsonResult CheckPANExist(string panNumber)
        {
            try
            {
                Log.Information("CheckPANExist method is triggered [panNumber={0}]", panNumber);
                bool panExistOrNot = _insuranceBO.FCheckPANExist(panNumber);
                Log.Information("CheckAadharNumberExist method is return {0}", panExistOrNot);
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

