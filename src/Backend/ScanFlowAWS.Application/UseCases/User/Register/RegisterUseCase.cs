using AutoMapper;
using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.User.Register.Interfaces;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Domain.Security;

namespace ScanFlowAWS.Application.UseCases.User.Register
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUserWriteOnlyRepository _userRepository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUseCase(IMapper mapper, IUserWriteOnlyRepository userRepository, IPasswordEncripter passwordEncripter, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordEncripter = passwordEncripter;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            ValidateRequest(request);


            var user = _mapper.Map<Domain.Entities.User>(request);

            user.PasswordHash = _passwordEncripter.Encrypt(request.Password);

            await _userRepository.AddAsync(user);
            await _unitOfWork.Commit();

            return new ResponseRegisterUserJson
            {
                Username = user.Username,
                Email = user.Email,
            };
        }

        private void ValidateRequest(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);

            }
        }

    }
}
