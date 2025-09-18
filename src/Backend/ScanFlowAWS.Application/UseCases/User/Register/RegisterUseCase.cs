using AutoMapper;
using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.User.Register.Interfaces;

namespace ScanFlowAWS.Application.UseCases.User.Register
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IMapper _mapper;

        public RegisterUseCase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
             ValidateRequest(request);

            return null;

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
