using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace InsuranceManagementSystemMVC.Repository
{
    public class LoginRepository : ILoginRepository
    {

        private readonly InsuranceContext _ctx;

        #region Constructor
        public LoginRepository(InsuranceContext ctx)
        {
            _ctx = ctx;
        }
        #endregion Constructor


        #region RegisterCustomer
        //POST:
        public int SLogin(Admin admin)
        {
            try
            {
                Log.Information("Login details in CustomerRepository {0}", admin);
                var _admin = _ctx.Admins.FirstOrDefault(c => c.Email == admin.Email && c.Password == admin.Password);

                if (_admin != null)
                {
                    return _admin.AdminId;
                }
            }
            catch (SqlException ex)
            {
                throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);                                  //throw new InsuranceManagementException(ex.Message, ex); ipdi eruntha Error page la sql exception view agum  
            }
            return 0;
        }
        #endregion Login

        //public IEnumerable<Login> Find(Expression<Func<Login, bool>> expression)
        //{
        //    throw new NotImplementedException();
        //}

        public int savechanges()
        {
            return _ctx.SaveChanges();
        }
    }
}
