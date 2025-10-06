using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Requests.Rekognition;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.DTOs.Responses.Rekognition;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces.Interfaces;
using ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces.Interface;

namespace ScanFlowAWS.API.Controllers
{
    /// <summary>
    /// Controller responsável por expor os endpoints relacionados ao Amazon Rekognition, 
    /// como análise de rostos e comparação de faces. 
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    public class RekognitionController : ControllerBase
    {
        /// <summary>
        /// Analisa as características faciais de uma imagem enviada pelo usuário.
        /// </summary>
        /// <param name="request">A imagem enviada pelo usuário</param>
        /// <param name="useCase">Caso de uso resposável pele lógica de análise</param>
        /// <returns>Retorna análise da imagem em um objeto <see cref="ResponseAnalyzeFacesJson"/> com os detalhes da análise ou um <see cref="ResponseErrorsJson"/> em caso de erro.</returns>
       
        [Authorize]
        [HttpPost("analyzefaces")] // POST api/rekognition/analyzefaces
        [ProducesResponseType(typeof(ResponseAnalyzeFacesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AnalyzeFaces([FromForm] RequestAnalyzeFacesJson request, [FromServices]IAnalyzeFacesUseCase useCase)
        {
            
            var result = await useCase.Execute(request);

            return Ok(result);
        }


        /// <summary>
        /// Compara duas imagens enviadas pelo usuário para verificar se são iguais. 
        /// </summary>
        /// <param name="request">Duas imagens enviadas pelo usuário para verificação.</param>
        /// <param name="useCase">Caso de uso respável pela logíca de comparação de imagens</param>
        /// <returns>Retorna a verificação em um objeto <see cref="ResponseCompareceFacesJson"/>com a porcentagem da comparação ou um <see cref="ResponseErrorsJson"/>em caso de erro.</returns>

        [Authorize]
        [HttpPost("compareimages")] // POST api/rekognition/comparecefaces
        [ProducesResponseType(typeof(ResponseCompareceFacesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompareImages([FromForm] RequestCompareceFacesJson request, [FromServices] ICompareceFaces useCase)
        {
            var result = await useCase.Execute(request);
            return Ok(result);
        }
    }
}
