using ScanFlowAWS.Application.Exceptions.ResourcesMassages;

namespace ScanFlowAWS.Application.Exceptions
{
    public class InvalidLoginException : ScanFlowAWSApplicationException
    {
       public InvalidLoginException() : base(ResourceMessageException.EMAIL_OR_PASSWORD_INVALID){ }
    }
}
