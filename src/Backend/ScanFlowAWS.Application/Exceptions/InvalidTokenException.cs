using ScanFlowAWS.Application.Exceptions.ResourcesMassages;

namespace ScanFlowAWS.Application.Exceptions
{
    public class InvalidTokenException : ScanFlowAWSApplicationException
    {
        public InvalidTokenException() : base(ResourceMessageException.TOKEN_INVALID) { }

    }
}
