using System.Runtime.Serialization;

namespace InsuranceManagementSystemMVC.InsuranceException
{
    public class RecordInsertionFailedException : Exception
    {
        public RecordInsertionFailedException()
        {
        }

        public RecordInsertionFailedException(string? message) : base(message)
        {
        }

        public RecordInsertionFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RecordInsertionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
