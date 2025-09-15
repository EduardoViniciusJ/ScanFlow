using Microsoft.AspNetCore.Http;

namespace ScanFlowAWS.Application.DTOs.Requests
{
    public class RequestAnalyzeFacesJson
    {
        public IFormFile ?file { get; set; } 
    }
}
