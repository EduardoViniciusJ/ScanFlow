using Microsoft.AspNetCore.Http;

namespace ScanFlowAWS.Application.DTOs.Requests
{
    public class RequestRekognition
    {
        public IFormFile ?file { get; set; } 
    }
}
