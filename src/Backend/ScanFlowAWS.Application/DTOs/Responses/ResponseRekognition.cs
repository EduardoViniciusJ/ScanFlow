namespace ScanFlowAWS.Application.DTOs.Responses
{
    public class ResponseRekognition
    {
        public string Label { get; set; } = string.Empty;
        public float Confidence { get; set; }
    }
}
