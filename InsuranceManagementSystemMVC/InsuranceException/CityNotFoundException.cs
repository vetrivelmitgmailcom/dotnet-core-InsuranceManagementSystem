using System.Runtime.Serialization;

namespace InsuranceManagementSystemMVC.InsuranceException
{
    public class CityNotFoundException : Exception
    {
        public CityNotFoundException()
        {
        }

        public CityNotFoundException(string? message) : base(message)
        {
        }

        public CityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
