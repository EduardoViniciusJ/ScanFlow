using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.DTOs.Requests.Token;
using ScanFlowAWS.Application.UseCases.User.Token.Interfaces;

namespace ScanFlowAWS.API.Controllers
{
    /// <summary>
    /// Controller para atualizações de tokens de acesso e refresh token. 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        /// <summary>
        ///  Gera um refresh token válido.
        /// </summary>
        /// <param name="token">Objeto contendo o refresh token enviado pelo cliente.</param>
        /// <param name="useCase">Serviço que executa a lógica de atualização do token.</param>
        /// <returns>Um objeto com um novo token gerado.</returns>
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] RequestTokenJson token, [FromServices] IRefreshTokenUseCase useCase)
        {
            var response = await useCase.Execute(token);
            return Ok(response);    
        }
        
    }
}
