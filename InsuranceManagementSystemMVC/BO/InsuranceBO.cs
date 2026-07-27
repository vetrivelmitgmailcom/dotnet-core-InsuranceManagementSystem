using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using InsuranceManagementSystemMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Serilog;


namespace InsuranceManagementSystemMVC.BO
{
    public class InsuranceBO
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly INomineeRepository _nomineeRepository;
        private readonly IPolicyRepository _policyRepository;
        private readonly IPaymentRepository _paymentRepository;

    


        #region Constructor
        public InsuranceBO(ILoginRepository loginRepository, ICustomerRepository customerRepository, IPolicyRepository policyRepository,INomineeRepository nomineeRepository,IPaymentRepository paymentRepository)
        {
            _loginRepository = loginRepository;
            _customerRepository = customerRepository;
            _policyRepository = policyRepository;
            _nomineeRepository = nomineeRepository;
            _paymentRepository = paymentRepository;

        }
        #endregion Constructor

        #region /**** Login Controller ****\
        #region Login
        //POST:
        public int FLogin(Admin admin)
        {
                Log.Information("Login details in InsuranceBo {0}", admin);
                return _loginRepository.SLogin(admin);
        }
        #endregion Login
        #endregion

        #region /**** Customer Controller ****\

        #region RegisterCustomer
        //POST:
        public long RegisterCustomer(Customer newCustomer)
        {
                Log.Information("Customer personal details in InsuranceBo {0}", newCustomer);
                return _customerRepository.RegisterCustomer(newCustomer);
        }
        #endregion RegisterCustomer


        #region UpdateCustomer
        //GET:
        public Customer? GetCustomerById(long customerId)
        {
                Log.Information("Fetching Customer Details in InsuranceBo [customerId={0}]", customerId);
                return _customerRepository.GetCustomerById(customerId);
        }



        //POST:
        public long UpdateCustomer(long CustomerId, Customer existCustomer)
        {
                Log.Information("Updating customer personal details in InsuranceBo {0}", existCustomer);
                return _customerRepository.UpdateCustomer(CustomerId, existCustomer);
        }
        #endregion UpdateCustomer



        #region CustomerInfo,GetCustomerInfo,CustomerInfoMore
        //GET:
        public IEnumerable<Customer> AllCustomerInformation()
        {
                Log.Information("Fetching Customers Details in InsuranceBo");
                return _customerRepository.AllCustomerInformation();
        }



        //GET:
        //-->GetCustomerInfo:GetCustomerInfoPartial



        //GET:
        public Customer? AdditionalCustomerInformation(long customerId)
        {
                Log.Information("Fetching Customer Details in InsuranceBo [customerId={0}]", customerId);
                return _customerRepository.AdditionalCustomerInformation(customerId);
        }
        #endregion  CustomerInfo,GetCustomerInfo,CustomerInfoMore



        #region PartialView {GetCustomerInfo:GetCustomerInfoPartial}
        //GET:
        public IEnumerable<Customer> FGetCustomerInfoPartial(long customerId)
        {
                Log.Information("Fetching Customer Details in InsuranceBo [customerId={0}]", customerId);
                return _customerRepository.SGetCustomerInfoPartial(customerId);
        }
        #endregion PartialViews


        #region DeleteCustomer
        //GET:
        public bool DeleteCustomer(long customerId)
        {
            Log.Information("Deleting Customer Details in InsuranceBo [customerId={0}]", customerId);
            return _customerRepository.DeleteCustomer(customerId);
        }
        #endregion


        #region DeletePolicy
        //GET:
        public bool DeletePolicy(long policyId)
        {
            Log.Information("Deleting Policy Details in InsuranceBo [policyId={0}]", policyId);
            return _policyRepository.DeletePolicy(policyId);
        }
        #endregion



        #region Select Box Data
        public List<SelectListItem> FGetCountries()
        {
                Log.Information("Fetching Countries in InsuranceBo");
                return _customerRepository.SGetCountries();
        }



        //GET:
        public List<SelectListItem> FGetStates(int countryId)
        {
                Log.Information("Fetching States in InsuranceBo [coiuntryId={0}]", countryId);
                return _customerRepository.SGetStates(countryId);
        }



        //GET:
        public List<SelectListItem> FGetCities(int stateId)
        {
                Log.Information("Fetching Cities in InsuranceBo [stateId={0}]", stateId);
                return _customerRepository.SGetCities(stateId);
        }
        #endregion Select Box Data


        #region validation
        //GET:
        public bool FCheckMobileNumberExist(long mobileNumber)
        {
                Log.Information("Fetching FCheckMobileNumberExist in InsuranceBo [mobileNumber={0}]", mobileNumber);
                return _customerRepository.SCheckMobileNumberExist(mobileNumber);
        }

        //GET:
        public bool FCheckEmailExist(string email)
        {
                Log.Information("Fetching FCheckEmailExist in InsuranceBo [email={0}]", email);
                return _customerRepository.SCheckEmailExist(email);
        }

        //GET:
        public bool FCheckAadharNumberExist(string aadharNumber)
        {
                Log.Information("Fetching FCheckAadharNumberExist in InsuranceBo [aadharNumber={0}]", aadharNumber);
                return _customerRepository.SCheckAadharNumberExist(aadharNumber);
        }


        //GET:
        public bool FCheckPANExist(string panNumber)
        {
                Log.Information("Fetching FCheckPANExist in InsuranceBo [panNumber={0}]", panNumber);
                return _customerRepository.SCheckPANExist(panNumber);
        }

        #endregion

        #endregion /**** Customer Controller ****\




        #region /**** Nominee Controller ****\

        #region NomineeInfo
        //GET:
        public IEnumerable<NomineeDetail> AllNomineeInformation()
        {
                Log.Information("Fetching Nominees Details in InsuranceBo");
                return _nomineeRepository.AllNomineeInformation();
        }
        #endregion NomineeInfo



        #region validation
        //GET:
        public bool FCheckMobileNumberExist_Nominee(long mobileNumber)
        {
                Log.Information("Fetching FCheckMobileNumberExist_Nominee in InsuranceBo [mobileNumber={0}]", mobileNumber);
                return _nomineeRepository.SCheckMobileNumberExist_Nominee(mobileNumber);
        }


        //GET:
        public bool FCheckAadharNumberExist_Nominee(string aadharNumber)
        {
                Log.Information("Fetching FCheckAadharNumberExist_Nominee in InsuranceBo [aadharNumber={0}]", aadharNumber);
                return _nomineeRepository.SCheckAadharNumberExist_Nominee(aadharNumber);

        }


        //GET:
        public bool FCheckPANExist_Nominee(string panNumber)
        {
                Log.Information("Fetching FCheckPANExist_Nominee in InsuranceBo [panNumber={0}]", panNumber);
                return _nomineeRepository.SCheckPANExist_Nominee(panNumber);
        }

        #endregion

        #endregion /**** Nominee Controller ****\




        #region /**** Policy Controller ****\


        #region AddPolicy,AdditionalPolicy
        //POST: {AddPolicy,AdditionalPolicy}
        public long AddPolicy(PolicyDetail newPolicy)
        {
                Log.Information("Customer policy details in InsuranceBo {0}", newPolicy);
                return _policyRepository.AddPolicy(newPolicy);
        }
        #endregion AddPolicy,AdditionalPolicy



        #region  PolicyInfo,GetPolicyInfo.PolicyInfoMore

        //GET:
        //-->PolicyInfo:PolicyInfoPartial


        //GET:
        //-->GetPolicyInfo:GetPolicyInfoPartial


        //GET:
        //public PolicyDetail? SPolicyInfoMore(long policyId)

        public PolicyDetail? AdditionalPolicyInformation(long policyId)
        {
                Log.Information("Fetching Policy Details in InsuranceBo [policyId={0}]", policyId);
            return _policyRepository.AdditionalPolicyInformation(policyId);
        }

        #endregion PolicyInfo,GetPolicyInfo.PolicyInfoMore


        #region PartialView {PolicyInfo:PolicyInfoPartial}  {GetPolicyInfo:GetPolicyInfoPartial}
        //GET:
        public IEnumerable<PolicyDetail> FPolicyInfoPartial()
        {
                Log.Information("Fetching Policy Details in InsuranceBo");
                return _policyRepository.SPolicyInfoPartial();
        }



        //GET:
        public IEnumerable<PolicyDetail> FGetPolicyInfoPartial(DateTime DateOfIssue, int AmountOfPeriod)
        {
                Log.Information("Fetching Policy Details in InsuranceBo [DateOfIssue={0},AmountOfPeriod={1}]", DateOfIssue, AmountOfPeriod);
                return _policyRepository.SGetPolicyInfoPartial(DateOfIssue, AmountOfPeriod);
        }
        #endregion  PartialView 



        #region Procedure
        //GET:
        public double FCalculatePremiumAmount(long CustomerId, int AmountOfPeriod, double InsuredDeclaredValue)
        {
                Log.Information("FCalculatePremiumAmount method triggered in InsuranceBo [CustomerId={0},AmountOfPeriod={1},InsuredDeclaredValue={2}]", CustomerId, AmountOfPeriod, InsuredDeclaredValue);
                return _policyRepository.SCalculatePremiumAmount(CustomerId, AmountOfPeriod, InsuredDeclaredValue);
        }
        #endregion Procedure


        #region Select Box Data
        public List<SelectListItem> FGetInsuranceType()
        {
                Log.Information("Fetching InsuranceType in InsuranceBo");
                return _policyRepository.SGetInsuranceType();
        }



        public List<SelectListItem> FGetModeOfPremium()
        {
                Log.Information("Fetching ModeOfPremium in InsuranceBo");
                return _policyRepository.SGetModeOfPremium();
        }


        public List<SelectListItem> FGetPaymentType()
        {
                Log.Information("Fetching PaymentType in InsuranceBo");
                return _policyRepository.SGetPaymentType();
        }


        public List<SelectListItem> FGetRelationship()
        {
                Log.Information("Fetching Relationship in InsuranceBo");
                return _policyRepository.SGetRelationship();
        }
        #endregion Select Box Data

        #endregion /**** Policy Controller ****\



        #region /**** Payment Controller ****\

        #region PaymentInfo
        //GET:
        public IEnumerable<Payment> AllPaymentInformation()
        {
                Log.Information("Fetching Payment Details in InsuranceBo");
                return _paymentRepository.AllPaymentInformation();
        }
        #endregion NomineeInfo
        #endregion
    }
}


