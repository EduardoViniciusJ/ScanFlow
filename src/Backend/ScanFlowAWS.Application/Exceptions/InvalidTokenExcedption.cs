using ScanFlowAWS.Application.Exceptions.ResourcesMassages;

namespace ScanFlowAWS.Application.Exceptions
{
    public class InvalidTokenExcedption : ScanFlowAWSApplicationException
    {
        public InvalidTokenExcedption() : base(ResourceMessageException.TOKEN_INVALID) { }

    }
}
