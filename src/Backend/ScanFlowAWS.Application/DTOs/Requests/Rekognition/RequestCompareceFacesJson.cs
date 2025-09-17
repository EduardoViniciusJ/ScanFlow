using Microsoft.AspNetCore.Http;

namespace ScanFlowAWS.Application.DTOs.Requests.Rekognition
{
    public class RequestCompareceFacesJson
    {
        public IFormFile? FileSource { get; set; }
        public IFormFile? FileTarget { get; set; }
    }
}
