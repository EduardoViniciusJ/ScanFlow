using AutoMapper;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.Exceptions;

namespace ScanFlowAWS.Application.UseCases.User.Register
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IMapper _mapper;

        public RegisterUseCase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
        {
            ValidateRequest(request);

            

            return new ResponseRegisterUserJson
            {
                Username = request.Username,
                Email = request.Email 
            };
        }

        Task<ResponseRegisterUserJson> IRegisterUseCase.Execute(RequestRegisterUserJson request)
        {
            throw new NotImplementedException();
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
