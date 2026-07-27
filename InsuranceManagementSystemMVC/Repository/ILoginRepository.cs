using InsuranceManagementSystemMVC.Models;
using System.Linq.Expressions;

namespace InsuranceManagementSystemMVC.Repository
{
    public interface ILoginRepository
    {

        #region Login

        //POST:
        public int SLogin(Admin admin);

        #endregion Login


        //public IEnumerable<Login> Find(Expression<Func<Login, bool>> expression)

        public int savechanges();
    }
}
