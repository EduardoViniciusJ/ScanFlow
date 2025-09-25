using ScanFlowAWS.Application.DTOs.Requests.Token;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.User.Token.Interfaces;
using ScanFlowAWS.Domain.Repositories.Token;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Domain.Services;

namespace ScanFlowAWS.Application.UseCases.User.Token
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {

        private readonly ITokenReadOnlyRepository _tokenReadOnlyRepository;
        private readonly ITokenWriteOnlyRepository _tokenWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenUseCase(ITokenReadOnlyRepository tokenReadOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository, ITokenService tokenService, IUnitOfWork unitOfWork, ITokenWriteOnlyRepository tokenWriteOnlyRepository)
        {
            _tokenReadOnlyRepository = tokenReadOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _tokenWriteOnlyRepository = tokenWriteOnlyRepository;   
        }

        public async Task<ResponseLoginUserJson> Execute(RequestTokenJson request)
        {
            var token = await _tokenReadOnlyRepository.GetByTokenAsync(request.RefreshToken);

            if (token is null || token.Expiration < DateTime.UtcNow || token.Type != "Refresh")
            {
                throw new InvalidLoginException();
            }

            var user = await _userReadOnlyRepository.GetByIdAsync(token.UserId);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var newAccessToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.RefreshToken(user);

            _tokenWriteOnlyRepository.Delete(token);
            await _tokenWriteOnlyRepository.AddAsync(newAccessToken);
            await _tokenWriteOnlyRepository.AddAsync(newAccessToken);
            await _unitOfWork.Commit();

            return new ResponseLoginUserJson
            {
                Username = user.Username,
                AccessToken = newAccessToken.TokenJWT,
                RefreshToken = newRefreshToken.TokenJWT
            };
        }
    }
}
