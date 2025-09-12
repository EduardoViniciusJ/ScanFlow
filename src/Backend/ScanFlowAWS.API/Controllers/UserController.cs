using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.UseCases.AmazonRekognition;
using ScanFlowAWS.Application.UseCases.User.Register;
using ScanFlowAWS.Infrastructure.DataAcess.Context;

namespace ScanFlowAWS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AnalyzeImageUseCase _analyzeImageUseCase;

        public UserController(AnalyzeImageUseCase analyzeImageUseCase)
        {
            _analyzeImageUseCase = analyzeImageUseCase;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhuma imagem enviada.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var result = await _analyzeImageUseCase.Execute(memoryStream.ToArray());

            return Ok(result);



        }


    }
}

