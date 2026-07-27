using System.Runtime.Serialization;

namespace InsuranceManagementSystemMVC.InsuranceException
{
    public class InsuranceManagementException : Exception
    {
        public InsuranceManagementException()
        {
        }

        public InsuranceManagementException(string? message) : base(message)
        {
        }

        public InsuranceManagementException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsuranceManagementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
