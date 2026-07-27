namespace InsuranceManagementSystemMVC.Models
{
    public class ErrorViewModel
    {

        public Exception Exception { get; set; } = null!;

        //RequestId: This property stores a unique identifier for the request that generated the exception. This can be used for debugging purposes to help identify the specific request that caused the error.
        public string? RequestId { get; set; }


        //ShowRequestId: This is a read-only property that returns true if the RequestId property is not null or empty. This is used to determine whether to display the RequestId value in the error message.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


        //Errorpath: This property stores the URL of the page or action that generated the exception. This can be useful for identifying the source of the error.
        public string? ErrorPath { get; set; }


        //ErrorMessage: This property stores the error message that is displayed to the user. This message can be customized to provide additional information about the exception or to provide guidance on how to resolve the error.
        public string? ErrorMessage { get; set; }


        //In this modified version of the class, a new property named StackTrace has been added. This property will allow us to store the stack trace information for the exception that caused the error.
        public string? StackTrace { get; set; }


        //InnerExceptionMessage: This property will allow us to store the message of any inner exception that caused the error.
        public string? InnerExceptionMessage { get; set; }

        //ErrorTime: This property will allow us to store the date and time when the error occurred. This can be helpful in identifying patterns or trends in error occurrences.
        public DateTime? ErrorTime { get; set; }


        //ServerName: This property will allow us to store the name of the server where the error occurred.This can be helpful in identifying issues that are specific to a particular server.
        public string? ServerName { get; set; }


        //AdditionalInfo: This property can be used to store any additional information that may be relevant to the error, but doesn't fit into one of the other categories. For example, it could be used to store information about the user's session, the browser or device they were using, or any other contextual information that may be helpful in understanding the error.    
        public string? AdditionalInfo { get; set; }




        //The ExceptionType property would store the fully qualified name of the exception type, while the TargetSite property would store the name of the method that threw the exception. The Data property would be a dictionary of additional key-value pairs associated with the exception, and the HResult property would be the HRESULT value associated with the exception.
        public string? ExceptionType { get; set; }

        public string? TargetSite { get; set; } = null!;

        public IDictionary<string, object>? Data { get; set; }

        public int? HResult { get; set; }



        //The Source property would store the name of the application or object that caused the error, while the LineNumber and FileName properties would store the line number and file name of the code that threw the exception. The HelpLink property could store a URL that provides more information about the exception, and the IsTransient property could be used to indicate whether the error is likely to be resolved if the operation is retried.
        public string? Source { get; set; }

        public int? LineNumber { get; set; }

        public string? FileName { get; set; }

        public string? HelpLink { get; set; }

        public bool? IsTransient { get; set; }


        //The ErrorId property could be used to uniquely identify the error for tracking purposes, while the ErrorCategory, ErrorSeverity, ErrorState, and ErrorNumber properties could provide more information about the type and severity of the error. These properties would be particularly useful if you are using a database to store error information and want to categorize and track errors more effectively.

        public string? ErrorId { get; set; }

        public string? ErrorCategory { get; set; }

        public string? ErrorSeverity { get; set; }

        public string? ErrorState { get; set; }

        public int? ErrorNumber { get; set; }

        //The HttpMethod property would store the HTTP method (e.g., GET, POST, PUT, DELETE) used for the request that caused the error. The RequestUrl and ReferrerUrl properties would store the URLs of the current and referring pages, respectively. The UserAgent property would store information about the user's browser and operating system, while the UserIpAddress property would store the user's IP address. These properties would be useful for debugging and diagnosing errors, especially if they are related to specific pages or user interactions.
        public string? HttpMethod { get; set; }

        public string? RequestUrl { get; set; }

        public string? ReferrerUrl { get; set; }

        public string? UserAgent { get; set; }

        public string? UserIpAddress { get; set; }

        //The UserId and UserName properties would store information about the user who caused the error, while the UserRoles and UserPermissions properties could store information about the user's roles and permissions, respectively. The IsAuthenticated property would indicate whether the user was authenticated at the time of the error. These properties would be useful for tracking errors that are specific to certain user roles or permissions, or for identifying patterns of errors related to certain types of user interactions.

        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public string? UserRoles { get; set; }

        public string? UserPermissions { get; set; }

        public bool? IsAuthenticated { get; set; }



    }
}