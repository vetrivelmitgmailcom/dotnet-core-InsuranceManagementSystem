using InsuranceManagementSystemMVC.Models;
using System.Linq.Expressions;

namespace InsuranceManagementSystemMVC.Repository
{
    public interface INomineeRepository
    {
        #region NomineeInfo
        //GET:
        public IEnumerable<NomineeDetail> AllNomineeInformation();

        #endregion NomineeInfo


        #region validation
        //GET:
        public bool SCheckMobileNumberExist_Nominee(long mobileNumber);


        //GET:
        public bool SCheckAadharNumberExist_Nominee(string aadharNumber);


        //GET:
        public bool SCheckPANExist_Nominee(string panNumber);
        #endregion


        public IEnumerable<NomineeDetail> Find(Expression<Func<NomineeDetail, bool>> expression);

        public int savechanges();
    }
}
