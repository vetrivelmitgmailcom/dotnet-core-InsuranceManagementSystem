using System.Runtime.Serialization;

namespace InsuranceManagementSystemMVC.InsuranceException
{
    public class StateNotFoundException : Exception
    {
        public StateNotFoundException()
        {
        }

        public StateNotFoundException(string? message) : base(message)
        {
        }

        public StateNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected StateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
