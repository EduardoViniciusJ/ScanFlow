using Microsoft.AspNetCore.Http;

namespace ScanFlowAWS.Application.DTOs.Requests.Rekognition
{
    public class RequestCompareceFacesJson
    {
        public IFormFile ?fileSource { get; set; }
        public IFormFile ?fileTarget { get; set; }
    }
}
