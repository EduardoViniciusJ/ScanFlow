using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.UseCases.User.Login.Interfaces;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Domain.Security;

namespace ScanFlowAWS.Application.UseCases.User.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly IPasswordEncripter _passwordEncripter;
        
        public LoginUseCase(IUserReadOnlyRepository userRepository, IPasswordEncripter password)
        {
            _userRepository = userRepository;
            _passwordEncripter = password;
        }

        public async Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);


            if (user is null || !_passwordEncripter.IsValid(request.Password, user.PasswordHash))
            {
                throw new Exception(); 
            }
            return new ResponseLoginUserJson
            {
                Username = user.Username,
            };           
        }
    }
}
