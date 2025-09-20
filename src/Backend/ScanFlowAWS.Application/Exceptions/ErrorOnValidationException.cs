namespace ScanFlowAWS.Application.Exceptions
{
    public class ErrorOnValidationException : ScanFlowAWSApplicationException
    {
        public IList<string> ErrorsMessage { get; set; }

        public ErrorOnValidationException(IList<string> erros) : base(string.Empty)
        {
            ErrorsMessage = erros;
        }
    }
}
