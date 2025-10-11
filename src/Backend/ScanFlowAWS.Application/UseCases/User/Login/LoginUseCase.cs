using Microsoft.AspNetCore.Http;
using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.User.Login.Interfaces;
using ScanFlowAWS.Domain.Repositories.Token;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Domain.Security;
using ScanFlowAWS.Domain.Services;
using System.Net.Http;

namespace ScanFlowAWS.Application.UseCases.User.Login
{
    /// <summary>
    /// Caso de uso responsável pela autenticação de usuários.
    /// Valida credenciais e gera tokens de acesso e refresh token.
    /// </summary>
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly ITokenService _tokenService;
        private readonly ITokenWriteOnlyRepository _tokenWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Construtor do caso de uso <see cref="LoginUseCase"/>.
        /// </summary>
        /// <param name="userRepository">Repositório somente leitura de usuários.</param>
        /// <param name="password">Serviço de validação e encriptação de senha.</param>
        /// <param name="tokenService">Serviço responsável por criar tokens JWT.</param>
        /// <param name="tokenWriteOnlyRepository">Repositório para salvar tokens emitidos.</param>
        /// <param name="unitOfWork">Unit of Work para salvar no banco de dados.</param>
        public LoginUseCase(
            IUserReadOnlyRepository userRepository,
            IPasswordEncripter password,
            ITokenService tokenService,
            ITokenWriteOnlyRepository tokenWriteOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordEncripter = password;
            _tokenService = tokenService;
            _tokenWriteOnlyRepository = tokenWriteOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Executa a autenticação de um usuário.
        /// </summary>
        /// <param name="request">Objeto contendo email e senha do usuário.</param>
        /// <returns>Objeto <see cref="ResponseLoginUserJson"/> com tokens de acesso e refresh.</returns>
        /// <exception cref="InvalidLoginException">Lançada quando o usuário não existe ou a senha é inválida.</exception>
        public async Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request, HttpContext context)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null || !_passwordEncripter.IsValid(request.Password, user.PasswordHash))
            {
                throw new InvalidLoginException();
            }

            var accessToken = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.RefreshToken(user);

            await _tokenWriteOnlyRepository.AddAsync(accessToken);
            await _tokenWriteOnlyRepository.AddAsync(refreshToken);
            await _unitOfWork.Commit();

            context.Response.Cookies.Append("accessToken", accessToken.TokenJWT, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = accessToken.Expiration
            });

            context.Response.Cookies.Append("refreshToken", refreshToken.TokenJWT, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = refreshToken.Expiration
            });


            return new ResponseLoginUserJson
            {
                Username = user.Username,
                Email = user.Email,
                Message = "Login realizado com sucesso!"
            };
        }
    }
}
