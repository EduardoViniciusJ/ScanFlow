using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.UseCases.User.Register;

namespace ScanFlowAWS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterUserJson request)
        {
            var register = new RegisterUseCase();

            var result = register.Execute(request);

            return Ok(result);  

        }

        
    }
}
