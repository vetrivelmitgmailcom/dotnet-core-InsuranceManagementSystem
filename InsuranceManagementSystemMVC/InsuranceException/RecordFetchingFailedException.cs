using System.Runtime.Serialization;

namespace InsuranceManagementSystemMVC.InsuranceException
{
    public class RecordFetchingFailedException : Exception
    {
        public RecordFetchingFailedException()
        {
        }

        public RecordFetchingFailedException(string? message) : base(message)
        {
        }

        public RecordFetchingFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RecordFetchingFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
