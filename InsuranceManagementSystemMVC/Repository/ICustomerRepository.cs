using InsuranceManagementSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;


namespace InsuranceManagementSystemMVC.Repository
{
    public interface ICustomerRepository
    {
        #region RegisterCustomer

        //POST:
        public long RegisterCustomer(Customer newCustomer);

        #endregion RegisterCustomer



        #region UpdateCustomer

        //GET:
        public Customer? GetCustomerById(long customerId);



        //POST:
        public long UpdateCustomer(long CustomerId, Customer existCustomer);

        #endregion UpdateCustomer



        #region CustomerInfo,GetCustomerInfo,CustomerInfoMore

        //GET:
        public IEnumerable<Customer> AllCustomerInformation();


        //GET:
        //-->GetCustomerInfo:GetCustomerInfoPartial


        //GET:
        public Customer? AdditionalCustomerInformation(long customerId);

        #endregion  CustomerInfo,GetCustomerInfo,CustomerInfoMore



        #region PartialView {GetCustomerInfo:GetCustomerInfoPartial}

        //GET:
        public IEnumerable<Customer> SGetCustomerInfoPartial(long customerId);

        #endregion PartialView



        #region DeleteCustomer

        //GET:
        public bool DeleteCustomer(long customerId);
        #endregion


        #region Select Box Data

        public List<SelectListItem> SGetCountries();

        //GET:
        public List<SelectListItem> SGetStates(int countryId);



        //GET:
        public List<SelectListItem> SGetCities(int stateId);

        #endregion Select Box Data


        #region validation
        //GET:
        public bool SCheckMobileNumberExist(long mobileNumber);


        //GET:
        public bool SCheckEmailExist(string email);
        
        //GET:
        public bool SCheckAadharNumberExist(string aadharNumber);


        //GET:
        public bool SCheckPANExist(string panNumber);
        #endregion


        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> expression);
        public int savechanges();
    }
}
