using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.Interface;

namespace ScanFlowAWS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RekognitionController : ControllerBase
    {   
        [HttpPost("analyzelabels")]
        [ProducesResponseType(typeof(ResponseRekognitionJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnalyzeLabels([FromForm] RequestRekognitionJson request, [FromServices]IRekognitionUseCase useCase)
        {
            var result = await useCase.ExecuteLabels(request);

            return Ok(result);
        }

        [HttpPost("analyzefaces")]
        [ProducesResponseType(typeof(ResponseRekognitionJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AnalyzeFaces([FromForm] RequestRekognitionJson request, [FromServices]IRekognitionUseCase useCase)
        {
            var result = await useCase.ExecuteFaces(request);

            return Ok(result);
        }
    }
}
