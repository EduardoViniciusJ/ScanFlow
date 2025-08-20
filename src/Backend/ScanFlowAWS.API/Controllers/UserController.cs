using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.API.Requests;
using ScanFlowAWS.API.Responses;

namespace ScanFlowAWS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RequestRegisterUserJson request)
        {
            
            return null;
        }

    }
}
