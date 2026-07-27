using InsuranceManagementSystemMVC.Models;
using System.Linq.Expressions;

namespace InsuranceManagementSystemMVC.Repository
{
    public interface IPaymentRepository
    {

        #region PaymentInfo
        //GET:
        public IEnumerable<Payment> AllPaymentInformation();

        #endregion PaymentInfo


        public IEnumerable<Payment> Find(Expression<Func<Payment, bool>> expression);

        public int savechanges();
    }
}
