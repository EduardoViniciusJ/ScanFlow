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
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly ITokenReadOnlyRepository _tokenReadOnlyRepository;
        private readonly ITokenWriteOnlyRepository _tokenWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Construtor do caso de uso <see cref="RefreshTokenUseCase"/>.
        /// </summary>
        /// <param name="tokenReadOnlyRepository">Repositório para leitura de tokens existentes.</param>
        /// <param name="userReadOnlyRepository">Repositório para leitura de usuários.</param>
        /// <param name="tokenService">Serviço responsável por criar tokens JWT e refresh tokens.</param>
        /// <param name="unitOfWork">Unit of Work para salvar no banco de dados.</param>
        /// <param name="tokenWriteOnlyRepository">Repositório para gravação e exclusão de tokens.</param>
        public RefreshTokenUseCase(
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

        /// <summary>
        /// Executa a atualização do token de acesso usando um Refresh Token.
        /// </summary>
        /// <param name="request">Objeto contendo o Refresh Token enviado pelo cliente.</param>
        /// <returns>Objeto <see cref="ResponseLoginUserJson"/> contendo o novo Access Token e Refresh Token.</returns>
        /// <exception cref="InvalidTokenException">
        /// Lançada quando o Refresh Token é inválido, expirado ou não corresponde a um usuário existente.
        /// </exception>
        public async Task<ResponseLoginUserJson> Execute(RequestTokenJson request)
        {
            // Busca o token no repositório
            var token = await _tokenReadOnlyRepository.GetByTokenAsync(request.RefreshToken);

            // Valida se o token existe, não expirou e é do tipo Refresh
            if (token is null || token.Expiration < DateTime.UtcNow || token.Type != "Refresh")
            {
                throw new InvalidTokenException();
            }

            // Busca o usuário relacionado ao token
            var user = await _userReadOnlyRepository.GetByIdAsync(token.UserId);
            if (user == null)
            {
                throw new InvalidTokenException();
            }

            // Gera novos tokens
            var newAccessToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.RefreshToken(user);

            // Remove o token antigo e salva os novos
            _tokenWriteOnlyRepository.Delete(token);
            await _tokenWriteOnlyRepository.AddAsync(newAccessToken);
            await _tokenWriteOnlyRepository.AddAsync(newRefreshToken); 
            await _unitOfWork.Commit();

            return new ResponseLoginUserJson
            {
                AccessToken = newAccessToken.TokenJWT,
                RefreshToken = newRefreshToken.TokenJWT
            };
        }
    }
}
