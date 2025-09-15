using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Requests.Rekognition;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.DTOs.Responses.Rekognition;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces.Interfaces;
using ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces.Interface;

namespace ScanFlowAWS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RekognitionController : ControllerBase
    {   
        [HttpPost("analyzefaces")]
        [ProducesResponseType(typeof(ResponseAnalyzeFacesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AnalyzeFaces([FromForm] RequestAnalyzeFacesJson request, [FromServices]IAnalyzeFacesUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpPost("comparecefaces")]
        [ProducesResponseType(typeof(ResponseCompareceFacesJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> CompareceFaces([FromForm] RequestCompareceFacesJson request, [FromServices] ICompareceFaces useCase)
        {
            var result = await useCase.Execute(request);
            return Ok(result);
        }
    }
}
