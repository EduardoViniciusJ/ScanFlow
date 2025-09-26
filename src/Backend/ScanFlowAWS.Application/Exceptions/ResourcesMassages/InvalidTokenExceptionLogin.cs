namespace ScanFlowAWS.Application.Exceptions.ResourcesMassages
{
    public class InvalidTokenExceptionLogin : ScanFlowAWSApplicationException
    {
        public InvalidTokenExceptionLogin() : base(ResourceMessageException.TOKEN_INVALID_LOGIN) { }
    }
}
