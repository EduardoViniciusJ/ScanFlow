using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.Exceptions;

namespace ScanFlowAWS.Application.UseCases.User.Register
{
    public class RegisterUseCase
    {


        public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
        {
            ValidateRequest(request);

            return new ResponseRegisterUserJson
            {
                Username = request.Username,
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
