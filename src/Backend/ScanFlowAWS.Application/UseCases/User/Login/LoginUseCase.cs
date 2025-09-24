using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.User.Login.Interfaces;
using ScanFlowAWS.Domain.Repositories.Token;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Domain.Security;
using ScanFlowAWS.Domain.Services;

namespace ScanFlowAWS.Application.UseCases.User.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly ITokenService _tokenService;
        private readonly ITokenWriteOnlyRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public LoginUseCase(IUserReadOnlyRepository userRepository, IPasswordEncripter password, ITokenService tokenService, ITokenWriteOnlyRepository tokenRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordEncripter = password;
            _tokenService = tokenService;   
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null || !_passwordEncripter.IsValid(request.Password, user.PasswordHash))
            {
                throw new InvalidLoginException();
            }

            var accessToken = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.RefreshToken(user);

            await _tokenRepository.AddAsync(accessToken);
            await _tokenRepository.AddAsync(refreshToken);
            await _unitOfWork.Commit();

            return new ResponseLoginUserJson
            {
                Username = user.Username,
                AccessToken = accessToken.TokenJWT,
            };           
        }
    }
}
