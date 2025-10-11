using Microsoft.AspNetCore.Http;
using ScanFlowAWS.Application.DTOs.Requests.Token;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.User.Token.Interfaces;
using ScanFlowAWS.Domain.Repositories.Token;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Domain.Services;

namespace ScanFlowAWS.Application.UseCases.User.Token
{
    /// <summary>
    /// Caso de uso responsável por atualizar tokens de acesso (Access Token)
    /// utilizando um Refresh Token válido.
    /// </summary>
    public class TokenUseCase : ITokenUseCase
    {
        private readonly ITokenReadOnlyRepository _tokenReadOnlyRepository;
        private readonly ITokenWriteOnlyRepository _tokenWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Construtor do caso de uso <see cref="TokenUseCase"/>.
        /// </summary>
        /// <param name="tokenReadOnlyRepository">Repositório para leitura de tokens existentes.</param>
        /// <param name="userReadOnlyRepository">Repositório para leitura de usuários.</param>
        /// <param name="tokenService">Serviço responsável por criar tokens JWT e refresh tokens.</param>
        /// <param name="unitOfWork">Unit of Work para salvar no banco de dados.</param>
        /// <param name="tokenWriteOnlyRepository">Repositório para gravação e exclusão de tokens.</param>
        public TokenUseCase(
            ITokenReadOnlyRepository tokenReadOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            ITokenWriteOnlyRepository tokenWriteOnlyRepository)
        {
            _tokenReadOnlyRepository = tokenReadOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _tokenWriteOnlyRepository = tokenWriteOnlyRepository;
        }

        public async Task Execute(HttpContext context)
        {
            if (!context.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                throw new InvalidTokenException();
            }

            var token = await _tokenReadOnlyRepository.GetByTokenAsync(refreshToken!);

            if (token is null || token.Expiration < DateTime.UtcNow || token.Type != "Refresh")
                throw new InvalidTokenException();

            var user = await _userReadOnlyRepository.GetByIdAsync(token.UserId);
            if (user == null)
                throw new InvalidTokenException();

            var newAccessToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.RefreshToken(user);

            _tokenWriteOnlyRepository.Delete(token);
            await _tokenWriteOnlyRepository.AddAsync(newAccessToken);
            await _tokenWriteOnlyRepository.AddAsync(newRefreshToken);
            await _unitOfWork.Commit();

            // Atualiza os cookies
            context.Response.Cookies.Append("accessToken", newAccessToken.TokenJWT, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = newAccessToken.Expiration
            });

            context.Response.Cookies.Append("refreshToken", newRefreshToken.TokenJWT, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = newRefreshToken.Expiration
            });
        }

    }
}
