using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.DTOs.Requests.Token;
using ScanFlowAWS.Application.UseCases.User.Token.Interfaces;

namespace ScanFlowAWS.API.Controllers
{
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
