using AutoMapper;
using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Linq.Expressions;
using System.Xml.Linq;


namespace InsuranceManagementSystemMVC.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InsuranceContext _ctx;

        #region Constructor
        public CustomerRepository(InsuranceContext ctx)
        {
            _ctx = ctx;
        }
        #endregion Constructor



        #region RegisterCustomer
        //POST:
        public long RegisterCustomer(Customer newCustomer)
        {
            try
            {
                Log.Information("Customer personal details in CustomerRepository {0}", newCustomer);
                _ctx.Customers.Add(newCustomer);
                int count = savechanges();

                if (count > 0)
                {
                    return newCustomer.CustomerId;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);                                  //throw new InsuranceManagementException(ex.Message, ex); ipdi eruntha Error page la sql exception view agum  
            }

            return 0;
        }
        #endregion RegisterCustomer



        #region UpdateCustomer
        //GET:
        public Customer? GetCustomerById(long customerId)
        {
            try
            {
                Log.Information("Fetching Customer Details in CustomerRepository [customerId={0}]", customerId);

                #region Method 1(Method syntax)
                var customer = _ctx.Customers
                   .Include(c => c.PersonalDetails)
                       .ThenInclude(p => p.Gender)
                   .Include(c => c.PersonalDetails)
                       .ThenInclude(p => p.MaritalStatus)
                   .Include(c => c.PersonalDetails)
                       .ThenInclude(p => p.City)
                   .Include(c => c.PersonalDetails)
                       .ThenInclude(p => p.State)
                   .Include(c => c.PersonalDetails)
                       .ThenInclude(p => p.Country)
                   .FirstOrDefault(c => c.CustomerId == customerId);
                #endregion


                #region Method 2 (Query with Method Syntax)

                //var customer = (from c in _ctx.Customers
                //                where c.CustomerId == customerId
                //                select c)
                //                .Include(c => c.PersonalDetails)
                //                    .ThenInclude(p => p.Gender)
                //                .Include(c => c.PersonalDetails)
                //                    .ThenInclude(p => p.MaritalStatus)
                //                .Include(c => c.PersonalDetails)
                //                    .ThenInclude(p => p.City)
                //                .Include(c => c.PersonalDetails)
                //                    .ThenInclude(p => p.State)
                //                .Include(c => c.PersonalDetails)
                //                    .ThenInclude(p => p.Country)
                //                .FirstOrDefault();
                #endregion

                if (customer != null)
                {
                    return customer;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
            return null;
        }



        //POST:
        public long UpdateCustomer(long CustomerId, Customer existCustomer)
        {
            try
            {
                Log.Information("Updating customer personal details in CustomerRepository {0}", existCustomer);
                var customer = _ctx.Customers.First(c => c.CustomerId == CustomerId);

                if (customer != null)
                {
                    #region Method 1
                    //******************************************** Method 1 ********************************************
                    var updatedProperties = existCustomer.GetType().GetProperties();

                    // Update the properties of the existing customer object
                    foreach (var property in updatedProperties)
                    {
                        var value = property.GetValue(existCustomer);
                        var existingProperty = customer.GetType().GetProperty(property.Name);
                        if (existingProperty != null && existingProperty.CanWrite)
                        {
                            existingProperty.SetValue(customer, value);
                        }
                    }
                    #endregion


                    #region Method 2
                    /*
                    //******************************************** Method 2 ********************************************
                    // Configure AutoMapper
                    var config = new MapperConfiguration(cfg => {
                         cfg.CreateMap<Customer, Customer>();
                     });
                     var mapper = config.CreateMapper();

                     // Map the updated customer object to the existing customer object
                     customer = mapper.Map<Customer, Customer>(existCustomer, customer);
                    */

                    #endregion


                    #region Method 3
                    /*
                    //******************************************** Method 3 ********************************************
                    // Get the properties of the updated customer object
                    var properties = existCustomer.GetType().GetProperties();

                    // Loop through the properties and update the corresponding property on the customer object
                    foreach (var property in properties)
                    {
                        var currentValue = property.GetValue(existCustomer);
                        if (currentValue != null)
                        {
                            var currentProperty = customer.GetType().GetProperty(property.Name);
                            currentProperty.SetValue(customer, currentValue);
                        }
                    }
                    */
                    #endregion



                    #region Method 4
                    /*
                    //******************************************** Method 4 ********************************************
                    //customer.FirstName = existCustomer.FirstName;
                    //customer.LastName = existCustomer.LastName;
                     //customer.StatusId = existCustomer.StatusId;
                    */
                    #endregion


                    int count = savechanges();

                    if (count > 0)
                    {
                        return existCustomer.CustomerId;
                    }
                }
                else
                {
                    throw new RecordFetchingFailedException("Failed to fetch the Record for Update customer.Please Contact Admin");
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
            return 0;
        }
        #endregion UpdateCustomer



        #region CustomerInfo,GetCustomerInfo,CustomerInfoMore
        //GET:
        public IEnumerable<Customer> AllCustomerInformation()
        {
            try
            {
                //Log.Information("Fetching Customers Details in CustomerRepository");
                #region Method 1 (Method syntax -> Egar Loading)
                //var customers = _ctx.Customers
                //  .Include(c => c.PersonalDetails)
                //      .ThenInclude(p => p.Gender)
                //  .Include(c => c.PersonalDetails)
                //      .ThenInclude(p => p.MaritalStatus)
                //  .Include(c => c.PersonalDetails)
                //      .ThenInclude(p => p.City)
                //  .Include(c => c.PersonalDetails)
                //     .ThenInclude(p => p.State)
                //  .Include(c => c.PersonalDetails)
                //      .ThenInclude(p => p.Country)
                //   //.Include(c => c.PolicyDetails)
                //  .Where(c => c.StatusId == 1)             //   Add the condition to filter customers with status ID 1 */  //ithu later ah add pannathu(work panni pakala)
                //  .AsEnumerable()
                //  .Select(c => c);



                //var filteredPolicyDetails = _ctx.PolicyDetails
                // .Where(pd => pd.StatusId == 1)
                // .ToList();
                //customer.PolicyDetails = filteredPolicyDetails;

                #endregion

                #region Method 2 (Query Syntax -> Join Two Table)

                //var customers = (from customer in _ctx.Customers
                //                 join personalDetail in _ctx.PersonalDetails on customer.CustomerId equals personalDetail.CustomerId
                /* where customer.StatusId == 1   // Add the condition to filter customers with status ID 1*/ //ithu later ah add pannathu(work panni pakala)
                                                                                                              //                 select new Customer
                                                                                                              //                 {
                                                                                                              //                     CustomerId = customer.CustomerId,
                                                                                                              //                     FirstName = customer.FirstName,
                                                                                                              //                     LastName = customer.LastName,
                                                                                                              //                     PersonalDetails = new List<PersonalDetail>
                                                                                                              //                     {
                                                                                                              //                         new PersonalDetail
                                                                                                              //                         {
                                                                                                              //                         Gender = personalDetail.Gender,
                                                                                                              //                         MobileNumber = personalDetail.MobileNumber,
                                                                                                              //                         Email= personalDetail.Email
                                                                                                              //                         }
                                                                                                              //                     }
                                                                                                              //                 });
                #endregion


                #region Method 3 (Query Syntax -> Join Three Table)
                var customers = (from customer in _ctx.Customers
                                 join personalDetail in _ctx.PersonalDetails on customer.CustomerId equals personalDetail.CustomerId into pd
                                 from personalDetail in pd.DefaultIfEmpty()
                                 join policyDetail in _ctx.PolicyDetails on customer.CustomerId equals policyDetail.CustomerId into pol
                                 from policyDetail in pol.DefaultIfEmpty()
                                 where customer.StatusId == 1 || policyDetail.StatusId == 1     // Add this condition to filter customers with status ID 1 //ithu later ah add pannathu(it is working)
                                 group new { customer, personalDetail } by customer.CustomerId into customerGroup
                                 select new Customer
                                 {
                                     CustomerId = customerGroup.Key,
                                     FirstName = customerGroup.First().customer.FirstName,
                                     LastName = customerGroup.First().customer.LastName,
                                     PersonalDetails = customerGroup.Select(c => new PersonalDetail        //(or)PersonalDetails = customerGroup.Select(c => c.personalDetail).ToList()
                                     {
                                         Gender = c.personalDetail.Gender,
                                         MobileNumber = c.personalDetail.MobileNumber,
                                         Email = c.personalDetail.Email
                                     }).ToList(),
                                     PolicyDetails = (from p in _ctx.PolicyDetails                         //(or)PolicyDetails = (from p in _ctx.PolicyDetails where p.CustomerId == customerGroup.Key select p).ToList()
                                                      where p.CustomerId == customerGroup.Key && p.StatusId == 1
                                                      select new PolicyDetail
                                                      {
                                                          PolicyId = p.PolicyId
                                                      }).ToList()
                                 })
                                 .ToList();

                //The DefaultIfEmpty() method is used in LINQ to provide a default value if a collection is empty or contains no elements.It returns a new collection that contains the original elements, and if the collection is empty, it adds a single element to the collection that has a default value for the type of the element.This can be useful in situations where you want to perform a left outer join and include all the elements from the left side of the join, even if there are no corresponding elements on the right side.
                #endregion

                if (customers.Any())
                {
                    return customers;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }

            return Enumerable.Empty<Customer>();
        }



        //GET:
        //-->GetCustomerInfo:GetCustomerInfoPartial



        //GET:
        public Customer? AdditionalCustomerInformation(long customerId)
        {
            try
            {
                Log.Information("Fetching Customer Details in CustomerRepository [customerId={0}]", customerId);

                var customer = _ctx.Customers
                  .Include(c => c.PersonalDetails)
                      .ThenInclude(p => p.Gender)
                  .Include(c => c.PersonalDetails)
                      .ThenInclude(p => p.MaritalStatus)
                  .Include(c => c.PersonalDetails)
                      .ThenInclude(p => p.City)
                  .Include(c => c.PersonalDetails)
                     .ThenInclude(p => p.State)
                  .Include(c => c.PersonalDetails)
                      .ThenInclude(p => p.Country)
                   .FirstOrDefault(c => c.CustomerId == customerId);    //.Where(c => c.CustomerId == customerId);

                var filteredPolicyDetails = _ctx.PolicyDetails
                 .Where(pd => pd.CustomerId == customerId && pd.StatusId == 1)
                 .Include(pd => pd.PolicyValue)
                 .ToList();

                if (customer != null)
                {
                    customer.PolicyDetails = filteredPolicyDetails;
                    return customer;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin",ex);
            }
            return null;
        }
        #endregion CustomerInfo,GetCustomerInfo,CustomerInfoMore



        #region PartialView {GetCustomerInfo:GetCustomerInfoPartial}
        //GET:
        public IEnumerable<Customer> SGetCustomerInfoPartial(long customerId)
        {

            try
            {
                Log.Information("Fetching Customer Details in CustomerRepository [customerId={0}]", customerId);

                #region Method 1 (Method syntax -> Egar Loading)
                //var customers = _ctx.Customers
                //  .Include(c => c.PersonalDetails)
                //      .ThenInclude(p => p.Gender)
                //  .Include(c => c.PersonalDetails)
                //      .ThenInclude(p => p.MaritalStatus)
                //  .Include(c => c.PersonalDetails)
                //      .ThenInclude(p => p.City)
                //  .Include(c => c.PersonalDetails)
                //     .ThenInclude(p => p.State)
                //  .Include(c => c.PersonalDetails)
                //      .ThenInclude(p => p.Country)
                //  .Include(c => c.PolicyDetails)
                /*.Where(c => c.StatusId == 1) // Add the condition to filter customers with status ID 1*/  //ithu later ah add pannathu(work panni pakala)
                                                                                                            //   .AsEnumerable()
                                                                                                            //   .Select(c => c).Where(e => e.CustomerId == customerId);
                #endregion

                #region Method 2 (Query Syntax -> Join Two Table)

                //var customers = (from customer in _ctx.Customers
                //                 join personalDetail in _ctx.PersonalDetails on customer.CustomerId equals personalDetail.CustomerId
                /* where customer.StatusId == 1   // Add the condition to filter customers with status ID 1*/ //ithu later ah add pannathu(work panni pakala)
                //                 select new Customer
                //                 {
                //                     CustomerId = customer.CustomerId,
                //                     FirstName = customer.FirstName,
                //                     LastName = customer.LastName,
                //                     PersonalDetails = new List<PersonalDetail>
                //                     {
                //                         new PersonalDetail
                //                         {
                //                         Gender = personalDetail.Gender,
                //                         MobileNumber = personalDetail.MobileNumber,
                //                         Email= personalDetail.Email
                //                         }
                //                     }
                //                 }).Where(e => e.CustomerId == customerId);
                #endregion



                #region Method 3 (Query Syntax 1-> Join Three Table)
                var customers = (from customer in _ctx.Customers
                                 join personalDetail in _ctx.PersonalDetails on customer.CustomerId equals personalDetail.CustomerId into pd
                                 from personalDetail in pd.DefaultIfEmpty()
                                 join policyDetail in _ctx.PolicyDetails on customer.CustomerId equals policyDetail.CustomerId into pol
                                 from policyDetail in pol.DefaultIfEmpty()
                                 where customer.StatusId == 1        // Add this condition to filter customers with status ID 1 //ithu later ah add pannathu(it is working)
                                 group new { customer, personalDetail } by customer.CustomerId into customerGroup
                                 select new Customer
                                 {
                                     CustomerId = customerGroup.Key,
                                     FirstName = customerGroup.First().customer.FirstName,
                                     LastName = customerGroup.First().customer.LastName,
                                     PersonalDetails = customerGroup.Select(c => new PersonalDetail        //(or)PersonalDetails = customerGroup.Select(c => c.personalDetail).ToList()
                                     {
                                         Gender = c.personalDetail.Gender,
                                         MobileNumber = c.personalDetail.MobileNumber,
                                         Email = c.personalDetail.Email
                                     }).ToList(),
                                     PolicyDetails = (from p in _ctx.PolicyDetails                         //(or)PolicyDetails = (from p in _ctx.PolicyDetails where p.CustomerId == customerGroup.Key select p).ToList()
                                                      where p.CustomerId == customerGroup.Key
                                                      select new PolicyDetail
                                                      {
                                                          PolicyId = p.PolicyId
                                                      }).ToList()
                                 })
                                 .Where(e => e.CustomerId == customerId)
                                 .ToList();

                //The DefaultIfEmpty() method is used in LINQ to provide a default value if a collection is empty or contains no elements.It returns a new collection that contains the original elements, and if the collection is empty, it adds a single element to the collection that has a default value for the type of the element.This can be useful in situations where you want to perform a left outer join and include all the elements from the left side of the join, even if there are no corresponding elements on the right side.
                #endregion

                #region Method 3 (Query Syntax 2 -> Join Three Table)

                //            var customers = _ctx.Customers
                //.Where(customer => customer.CustomerId == customerId && customer.StatusId == 1)       // Add this condition to filter customers with status ID 1 //ithu later ah add pannathu(it is working)
                //.GroupJoin(
                //    _ctx.PersonalDetails,
                //    customer => customer.CustomerId,
                //    personalDetail => personalDetail.CustomerId,
                //    (customer, personalDetails) => new { Customer = customer, PersonalDetails = personalDetails })
                //.SelectMany(
                //    cp => cp.PersonalDetails.DefaultIfEmpty(),
                //    (customer, personalDetail) => new { Customer = customer.Customer, PersonalDetail = personalDetail })
                //.GroupJoin(
                //    _ctx.PolicyDetails,
                //    cp => cp.Customer.CustomerId,
                //    policyDetail => policyDetail.CustomerId,
                //    (cp, policyDetails) => new { Customer = cp.Customer, PersonalDetail = cp.PersonalDetail, PolicyDetails = policyDetails })
                //.SelectMany(
                //    cpl => cpl.PolicyDetails.DefaultIfEmpty(),
                //    (cpl, policyDetail) => new { cpl.Customer, cpl.PersonalDetail, PolicyDetail = policyDetail })
                //.GroupBy(cpp => cpp.Customer.CustomerId)
                //.Select(
                //    cg => new Customer
                //    {
                //        CustomerId = cg.Key,
                //        FirstName = cg.First().Customer.FirstName,
                //        LastName = cg.First().Customer.LastName,
                //        PersonalDetails = cg.Select(cpp => new PersonalDetail
                //        {
                //            Gender = cpp.PersonalDetail.Gender,
                //            MobileNumber = cpp.PersonalDetail.MobileNumber,
                //            Email = cpp.PersonalDetail.Email
                //        }).ToList(),
                //        PolicyDetails = cg.Where(cpp => cpp.PolicyDetail != null).Select(cpp => new PolicyDetail
                //        {
                //            PolicyId = cpp.PolicyDetail.PolicyId
                //        }).ToList()
                //    })
                //.ToList();
                #endregion


                if (customers.Any())
                {
                    return customers;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
            return Enumerable.Empty<Customer>();
        }
        #endregion PartialView





        #region DeleteCustomer
        //GET:
        public bool DeleteCustomer(long customerId)
        {
            try
            {
                Log.Information("Deleting customer details in CustomerRepository [customerId={0}]", customerId);
                var customer = _ctx.Customers.Include(c => c.PolicyDetails).FirstOrDefault(c => c.CustomerId == customerId);

                if (customer != null)
                {
                    // Set customer status to 0
                    customer.StatusId = 0;

                    // Set policy status of all policies associated with the customer to 0
                    foreach (var policy in customer.PolicyDetails)
                    {
                        policy.StatusId = 0;
                    }
                    _ctx.SaveChanges();
                
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
            return false;
        }
        #endregion



        #region Select Box Data
        public List<SelectListItem> SGetCountries()
        {
            try
            {
                Log.Information("Fetching Countries in CustomerRepository");
                List<CountryMaster> countries = _ctx.CountryMasters.ToList();

                var listCountries = countries
                .OrderBy(n => n.Country)
                .Select(c => new SelectListItem()
                {
                    Value = c.CountryId.ToString(),
                    Text = c.Country
                }).ToList();


                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "..Select Country..",
                    Disabled = true,
                    Selected = true
                };

                listCountries.Insert(0, defItem);

                if (listCountries.Any())
                {
                    return listCountries;
                }
                else
                {
                    return new List<SelectListItem>();
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
        }



        //GET:
        public List<SelectListItem> SGetStates(int countryId)
        {
            try
            {
                Log.Information("Fetching States in CustomerRepository [countryId={0}]",countryId);
                var listStates = _ctx.StateMasters
                    .Where(c => c.CountryId == countryId)
                    .OrderBy(n => n.State)
                .Select(c => new SelectListItem()
                {
                    Value = c.StateId.ToString(),
                    Text = c.State,
                }).ToList();


                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "..Select State..",
                    Disabled = true,
                    Selected = true
                };

                listStates.Insert(0, defItem);

                if (listStates.Any())
                {
                    return listStates;
                }
                else
                {
                    return new List<SelectListItem>();
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
        }



        //GET:
        public List<SelectListItem> SGetCities(int stateId)
        {
            try
            {
                Log.Information("Fetching Cities in CustomerRepositor [stateId={0}]",stateId);
                List<CityMaster> cities = _ctx.CityMasters.ToList();

                var listCities = cities
                .Where(s => s.StateId == stateId)
                .OrderBy(n => n.City)
                .Select(c => new SelectListItem()
                {
                    Value = c.CityId.ToString(),
                    Text = c.City
                }).ToList();


                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "..Select City..",
                    Disabled = true,
                    Selected = true
                };

                listCities.Insert(0, defItem);

                if (listCities.Any())
                {
                    return listCities;
                }
                else
                {
                    return new List<SelectListItem>();
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
        }
        #endregion Select Box Data


        #region validation
        //GET:
        public bool SCheckMobileNumberExist(long mobileNumber)
        {
            try
            {
                Log.Information("Fetching SCheckMobileNumberExist in CustomerRepository [mobileNumber={0}]", mobileNumber);
                var customer = _ctx.PersonalDetails
                   .AsEnumerable()
                   .FirstOrDefault(c => c.MobileNumber == mobileNumber);
                if (customer != null)
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
            return false;
        }

        //GET:
        public bool SCheckEmailExist(string email)
        {
            try
            {
                Log.Information("Fetching SCheckEmailExist in CustomerRepository [email={0}]", email);
                var customer = _ctx.PersonalDetails
                 .AsEnumerable()
                 .FirstOrDefault(c => c.Email == email);
                if (customer != null)
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }

            return false;
        }


        //GET:
        public bool SCheckAadharNumberExist(string aadharNumber)
        {
            try
            {
                Log.Information("Fetching SCheckAadharNumberExist in CustomerRepository [aadharNumber={0}]", aadharNumber);
                var customer = _ctx.PersonalDetails
             .AsEnumerable()
             .FirstOrDefault(c => c.AadharNumber == aadharNumber);
                if (customer != null)
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }

            return false;
        }


        //GET:
        public bool SCheckPANExist(string panNumber)
        {
            try
            {
                Log.Information("Fetching SCheckPANExist in CustomerRepository [panNumber={0}]", panNumber);
                var customer = _ctx.PersonalDetails
           .AsEnumerable()
           .FirstOrDefault(c => c.PanNumber == panNumber);
                if (customer != null)
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }

            return false;
        }

        #endregion


        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> expression)
        {
            throw new NotImplementedException();
        }


        public int savechanges()
        {
            return _ctx.SaveChanges();
        }
    }
}
