namespace ScanFlowAWS.Application.DTOs.Responses
{
    public class ResponseAnalyzeFacesJson
    {
        public float Confidence { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
