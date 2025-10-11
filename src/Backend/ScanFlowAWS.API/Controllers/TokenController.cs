using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.UseCases.User.Token.Interfaces;

namespace ScanFlowAWS.API.Controllers
{
    /// <summary>
    /// Controller para atualizações de tokens de acesso e refresh token. 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromServices] ITokenUseCase tokenUseCase)
        {
            await tokenUseCase.Execute(HttpContext);
            return Ok();
        }

    }
}
