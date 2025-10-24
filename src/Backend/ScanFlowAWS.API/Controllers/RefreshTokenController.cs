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

        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] RequestTokenJson token, [FromServices] IRefreshTokenUseCase useCase)
        {
            var response = await useCase.Execute(token);
            return Ok(response);    
        }
        
    }
}
