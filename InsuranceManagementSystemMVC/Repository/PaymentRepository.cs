using InsuranceManagementSystemMVC.InsuranceException;
using InsuranceManagementSystemMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace InsuranceManagementSystemMVC.Repository
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly InsuranceContext _ctx;

        #region Constructor
        public PaymentRepository(InsuranceContext ctx)
        {
            _ctx = ctx;
        }
        #endregion Constructor



        #region PaymentInfo
        //GET:
        public IEnumerable<Payment> AllPaymentInformation()
        {
            try
            {
                Log.Information("Fetching Payment Details in PaymentRepository");

                var payments = _ctx.Payments
              .Include(c => c.PaymentType)
              .Include(c => c.Premium.Policy)
              .Where(c => c.Premium.Policy.StatusId==1)
              .Select(c => c).ToList();

                #region Exception
                //var payments = _ctx.Payments
                //    .Include(c => c.PaymentType)
                //    .Include(c => c.Premium.Policy.PolicyId)   //error occured
                //    .Select(c => c).ToList();
                #endregion


                if (payments.Any())
                {
                    return payments;
                }
            }
            catch (SqlException ex)
            {
                 throw new InsuranceManagementException("Error Occured,Please contact Admin", ex);
            }

            return Enumerable.Empty<Payment>();
        }
        #endregion PaymentInfo


        public IEnumerable<Payment> Find(Expression<Func<Payment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public int savechanges()
        {
            return _ctx.SaveChanges();
        }
    }
}
