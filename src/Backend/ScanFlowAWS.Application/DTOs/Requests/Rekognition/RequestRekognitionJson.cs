using Microsoft.AspNetCore.Http;

namespace ScanFlowAWS.Application.DTOs.Requests
{
    public class RequestRekognitionJson
    {
        public IFormFile ?file { get; set; } 
    }
}
