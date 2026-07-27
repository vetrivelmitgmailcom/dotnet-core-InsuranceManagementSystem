using InsuranceManagementSystemMVC.Models;
using InsuranceManagementSystemMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace InsuranceManagementSystemMVC.Repository
{
    public interface IPolicyRepository
    {


        #region AddPolicy,AdditionalPolicy
        //POST: {AddPolicy,AdditionalPolicy}
        public long AddPolicy(PolicyDetail newPolicy);
        #endregion AddPolicy,AdditionalPolicy



        #region  PolicyInfo,GetPolicyInfo.PolicyInfoMore

        //GET:
        //-->PolicyInfo:PolicyInfoPartial


        //GET:
        //-->GetPolicyInfo:GetPolicyInfoPartial


        //GET:
        public PolicyDetail? AdditionalPolicyInformation(long policyId);

        #endregion PolicyInfo,GetPolicyInfo.PolicyInfoMore




        #region PartialView {PolicyInfo:PolicyInfoPartial}  {GetPolicyInfo:GetPolicyInfoPartial}
        //GET:
        public IEnumerable<PolicyDetail> SPolicyInfoPartial();

        //GET:
        public IEnumerable<PolicyDetail> SGetPolicyInfoPartial(DateTime DateOfIssue, int AmountOfPeriod);

        #endregion PartialView


        #region Procedure
        public double SCalculatePremiumAmount(long CustomerId, int AmountOfPeriod, double InsuredDeclaredValue);
        #endregion Procedure



        #region DeletePolicy

        //GET:
        public bool DeletePolicy(long policyId);
        #endregion

        #region Select Box Data
        public List<SelectListItem> SGetInsuranceType();
        public List<SelectListItem> SGetModeOfPremium();
        public List<SelectListItem> SGetPaymentType();
        public List<SelectListItem> SGetRelationship();
        #endregion Select Box Data



        public IEnumerable<PolicyDetail> Find(Expression<Func<PolicyDetail, bool>> expression);


        public int savechanges();
    }
}