namespace ScanFlowAWS.Application.Exceptions
{
    public class ErrorOnValidationException : ScanFlowAWSApplicationException
    {
        public IList<string> ErrorsMessage { get; set; }

        public ErrorOnValidationException(IList<string> erros)
        {
            ErrorsMessage = erros;
        }
    }
}
