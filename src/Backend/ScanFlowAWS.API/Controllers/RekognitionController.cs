using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.UseCases.AmazonRekognition;

namespace ScanFlowAWS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RekognitionController : ControllerBase
    {

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeImage([FromForm] RequestRekognition request, [FromServices]IRekognitionUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }
      
     

       



    }
}
