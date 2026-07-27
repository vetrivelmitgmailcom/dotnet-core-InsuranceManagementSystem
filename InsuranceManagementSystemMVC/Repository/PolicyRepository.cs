using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace InsuranceManagementSystemMVC.Repository
{
    public class PolicyRepository : IPolicyRepository
    {

        private readonly InsuranceContext _ctx;

        #region Constructor
        public PolicyRepository(InsuranceContext ctx)
        {
            _ctx = ctx;
        }
        #endregion Constructor



        #region AddPolicy,AdditionalPolicy
        //POST: {AddPolicy,AdditionalPolicy}
        public long AddPolicy(PolicyDetail newPolicy)
        {
            try
            {
                Log.Information("Customer policy details in PolicyRepository {0}", newPolicy);
                _ctx.PolicyDetails.Add(newPolicy);


                int count = savechanges();

                if (count > 0)
                {
                    return newPolicy.PolicyId;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }

            return 0;
        }
        #endregion AddPolicy,AdditionalPolicy
            


        #region  PolicyInfo,GetPolicyInfo,PolicyInfoMore

        //GET:
        //-->PolicyInfo:PolicyInfoPartial



        //GET:
        //-->GetPolicyInfo:GetPolicyInfoPartial



        //GET:
        public PolicyDetail? AdditionalPolicyInformation(long policyId)
        {
            try
            {
                Log.Information("Fetching policy Details in PolicyRepository [policyId={0}]", policyId);

                var policy = _ctx.PolicyDetails
              .Include(c => c.Customer)
              .Include(c => c.PolicyValue)
                  .ThenInclude(c => c.ModeOfPremium)
              .Include(c => c.PolicyValue)
                  .ThenInclude(c => c.Payments)
                  .ThenInclude(c => c.PaymentType)
              .Include(c => c.NomineeDetails)
                .ThenInclude(c => c.Gender)
              .Include(c => c.NomineeDetails)
                .ThenInclude(c => c.Relationship)
              .Include(c => c.Status)
              .Include(c => c.Insurance)

               .AsEnumerable()
               .FirstOrDefault(c => c.PolicyId == policyId);    //.Where(c => c.CustomerId == customerId);




                if (policy != null)
                {
                    return policy;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
            return null;

        }

        #endregion PolicyInfo,GetPolicyInfo.PolicyInfoMore



        #region PartialView {PolicyInfo:PolicyInfoPartial}  {GetPolicyInfo:GetPolicyInfoPartial}
        //GET:
        public IEnumerable<PolicyDetail> SPolicyInfoPartial()
        {

            try
            {
                Log.Information("Fetching Policy Details in PolicyRepository");

                var policyList = _ctx.PolicyDetails
              .Include(c => c.Customer)
              .Include(c => c.PolicyValue)
              .Where(c => c.StatusId == 1) 
              .AsEnumerable()
              .Select(c => c);

                if (policyList.Any())
                {
                    return policyList;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
            return Enumerable.Empty<PolicyDetail>();
        }


        //GET:
        public IEnumerable<PolicyDetail> SGetPolicyInfoPartial(DateTime DateOfIssue, int AmountOfPeriod)
        {
            try
            {
                Log.Information("Fetching Policy Details in PolicyRepository [DateOfIssue={0},AmountOfPeriod={1}]", DateOfIssue, AmountOfPeriod);

                var policyList = _ctx.PolicyDetails
              .Include(c => c.Customer)
              .Include(c => c.PolicyValue)
              .Where(c => c.StatusId == 1) 
              .AsEnumerable()
              .Select(c => c).Where(e => e.DateOfIssue == DateOfIssue && e.PolicyValue.AmountOfPeriod == AmountOfPeriod);


                if (policyList.Any())
                {
                    return policyList;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
            return Enumerable.Empty<PolicyDetail>();
        }

        #endregion PartialView



        #region Procedure
        //GET:
        public double SCalculatePremiumAmount(long CustomerId, int AmountOfPeriod, double InsuredDeclaredValue)
        {
            try
            {
                Log.Information("SCalculatePremiumAmount method triggered in PolicyRepository [CustomerId={0},AmountOfPeriod={1},InsuredDeclaredValue={2}]", CustomerId, AmountOfPeriod, InsuredDeclaredValue);

                SqlParameter[] parameters = new SqlParameter[]
                   {
                    new SqlParameter("@customer_id", SqlDbType.BigInt) { Value = CustomerId },
                    new SqlParameter("@amount_of_period", SqlDbType.Int) { Value = AmountOfPeriod },
                    new SqlParameter("@insured_declared_value", SqlDbType.Decimal) { Precision = 10, Scale = 2, Value = InsuredDeclaredValue },
                    new SqlParameter("@premium_to_be_paid", SqlDbType.Decimal) { Precision = 10, Scale = 2, Direction = ParameterDirection.Output }
                   };

                string query = "EXEC calculating_premium_Amount @customer_id, @amount_of_period, @insured_declared_value, @premium_to_be_paid OUTPUT";
                _ctx.Database.ExecuteSqlRaw(query, parameters);

                decimal premiumToBePaid = (decimal)parameters[3].Value;

                return Convert.ToDouble(premiumToBePaid);
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
        }
        #endregion Procedure




        #region DeleteCustomer
        //GET:
        public bool DeletePolicy(long policyId)
        {
            try
            {
                Log.Information("Deleting policy details in PolicyRepository [policyId={0}]", policyId);
                var policy = _ctx.PolicyDetails.FirstOrDefault(c => c.PolicyId == policyId);

                if (policy != null)
                {
                    // Set policy status to 0
                    policy.StatusId = 0;

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
        public List<SelectListItem> SGetInsuranceType()
        {
            try
            {
                Log.Information("Fetching InsuranceType in PolicyRepository");
                List<InsuranceTypeMaster> insuranceType = _ctx.InsuranceTypeMasters.ToList();

                var listInsuranceType = insuranceType.Select(c => new SelectListItem()
                {
                    Value = c.InsuranceId.ToString(),
                    Text = c.InsuranceType
                }).ToList();


                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "..select..",
                    Disabled = true,
                    Selected = true
                };

                listInsuranceType.Insert(0, defItem);

                return listInsuranceType;
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
        }



        public List<SelectListItem> SGetModeOfPremium()
        {
            try
            {
                Log.Information("Fetching ModeOfPremium in PolicyRepository");
                List<ModeOfPremiumMaster> modeOfPremium = _ctx.ModeOfPremiumMasters.ToList();

                var listModeOfPremium = modeOfPremium.Select(c => new SelectListItem()
                {
                    Value = c.ModeOfPremiumId.ToString(),
                    Text = c.ModeOfPremium
                }).ToList();


                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "..select..",
                    Disabled = true,
                    Selected = true
                };

                listModeOfPremium.Insert(0, defItem);

                return listModeOfPremium;
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
        }



        public List<SelectListItem> SGetPaymentType()
        {
            try
            {
                Log.Information("Fetching PaymentType in PolicyRepository");
                List<PaymentTypeMaster> paymentType = _ctx.PaymentTypeMasters.ToList();

                var listPaymentType = paymentType.Select(c => new SelectListItem()
                {
                    Value = c.PaymentTypeId.ToString(),
                    Text = c.PaymentType
                }).ToList();


                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "..select..",
                    Disabled = true,
                    Selected = true
                };

                listPaymentType.Insert(0, defItem);

                return listPaymentType;
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
        }


        public List<SelectListItem> SGetRelationship()
        {
            try
            {
                Log.Information("Fetching RelationshipType in PolicyRepository");
                List<RelationshipMaster> relationship = _ctx.RelationshipMasters.ToList();

                var listRelationship = relationship.Select(c => new SelectListItem()
                {
                    Value = c.RelationshipId.ToString(),
                    Text = c.Relationship
                }).ToList();


                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "..select..",
                    Disabled = true,
                    Selected = true
                };

                listRelationship.Insert(0, defItem);

                return listRelationship;
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }
        }

        #endregion Select Box Data



        public IEnumerable<PolicyDetail> Find(Expression<Func<PolicyDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }


        public int savechanges()
        {
            return _ctx.SaveChanges();
        }

    }
}
