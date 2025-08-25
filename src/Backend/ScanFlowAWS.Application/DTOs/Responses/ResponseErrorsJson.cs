namespace ScanFlowAWS.Application.DTOs.Responses
{
    public class ResponseErrorsJson
    {
        public IList<string> ErrorsMessage { get; set; }

        public ResponseErrorsJson(IList<string> errors)
        {
            ErrorsMessage = errors; 
        }

        public ResponseErrorsJson(string error)
        {
            ErrorsMessage = new List<string>()
           {
               error
           };
        }
    }
}
