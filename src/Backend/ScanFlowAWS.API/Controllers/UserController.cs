using Microsoft.AspNetCore.Mvc;
using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.UseCases.User.Login.Interfaces;
using ScanFlowAWS.Application.UseCases.User.Register.Interfaces;

namespace ScanFlowAWS.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de usuários,
    /// </summary>
    
     
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="useCase">
        /// Caso de uso responsável pela lógica de registro de usuários.
        /// </param>
        /// <param name="request">
        /// Objeto contendo os dados necessários para registrar um novo usuário.
        /// </param>
        /// <returns>
        /// Retorna <see cref="ResponseRegisterUserJson"/> em caso de sucesso (201 Created),
        /// ou <see cref="ResponseErrorsJson"/> em caso de falha de validação (400 Bad Request).
        /// </returns>
        [HttpPost("register")] // Endpoint: POST api/user/register
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUseCase useCase,       
            [FromBody] RequestRegisterUserJson request    
        )
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result); 
        }

        /// <summary>
        /// Autentica e faz o login de um usuário já registrado no sistema.
        /// </summary>
        /// <param name="useCase">
        /// Caso de uso responsável pela lógica de login de usuários.
        /// </param>
        /// <param name="request">
        /// Objeto contendo as credenciais de login.
        /// </param>
        /// <returns>
        /// Retorna <see cref="ResponseLoginUserJson"/> em caso de sucesso (200 OK),
        /// ou <see cref="ResponseErrorsJson"/> em caso de falha de autenticação (401 Unauthorized).
        /// </returns>
        [HttpPost("login")] // Endpoint: POST api/user/login
        [ProducesResponseType(typeof(ResponseLoginUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [FromServices] ILoginUseCase useCase,          
            [FromBody] RequestLoginUserJson request        
        )
        {
            var result = await useCase.Execute(request, HttpContext);
            return Ok(result); 
        }
    }
}

