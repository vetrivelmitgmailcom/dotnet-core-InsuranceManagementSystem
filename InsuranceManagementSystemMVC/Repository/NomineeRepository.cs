using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace InsuranceManagementSystemMVC.Repository
{
    public class NomineeRepository : INomineeRepository
    {

        private readonly InsuranceContext _ctx;

        #region Constructor
        public NomineeRepository(InsuranceContext ctx)
        {
            _ctx = ctx;
        }
        #endregion Constructor



        #region NomineeInfo
        //GET:
        public IEnumerable<NomineeDetail> AllNomineeInformation()
        {
            try
            {
                Log.Information("Fetching Nominees Details in NomineeRepository");
                var nominees = _ctx.NomineeDetails
              .Include(c => c.Policy)
              .ThenInclude(p => p.Customer)
              .Include(c => c.Relationship)
              .Include(c => c.Gender)   
              .Where(c=>c.Policy.StatusId==1)
              .Select(c => c).ToList();


                if (nominees.Any())
                {
                    return nominees;
                }
            }
            catch (SqlException ex)
            {
                                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }

            return Enumerable.Empty<NomineeDetail>();
        }
        #endregion NomineeInfo


        #region validation
        //GET:
        public bool SCheckMobileNumberExist_Nominee(long mobileNumber)
        {
            Log.Information("Fetching SCheckMobileNumberExist_Nominee in NomineeRepository [mobileNumber={0}]", mobileNumber);
            var customer = _ctx.NomineeDetails
                   .AsEnumerable()
                   .FirstOrDefault(c => c.MobileNumber == mobileNumber);
            if (customer != null)
            {
                return true;
            }

            return false;
        }


        //GET:
        public bool SCheckAadharNumberExist_Nominee(string aadharNumber)
        {
            Log.Information("Fetching SCheckAadharNumberExist_Nominee in NomineeRepository [aadharNumber={0}]", aadharNumber);

            var customer = _ctx.NomineeDetails
                 .AsEnumerable()
                 .FirstOrDefault(c => c.AadharNumber == aadharNumber);
            if (customer != null)
            {
                return true;
            }

            return false;
        }


        //GET:
        public bool SCheckPANExist_Nominee(string panNumber)
        {
            Log.Information("Fetching SCheckPANExist_Nominee in NomineeRepository [panNumber={0}]", panNumber);

            var customer = _ctx.NomineeDetails
                   .AsEnumerable()
                   .FirstOrDefault(c => c.PanNumber == panNumber);
            if (customer != null)
            {
                return true;
            }

            return false;
        }

        #endregion



        public IEnumerable<NomineeDetail> Find(Expression<Func<NomineeDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }


        public int savechanges()
        {
            return _ctx.SaveChanges();
        }

    }
}
