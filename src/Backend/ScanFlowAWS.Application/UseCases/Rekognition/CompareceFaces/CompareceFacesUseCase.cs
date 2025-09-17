using ScanFlowAWS.Application.DTOs.Requests.Rekognition;
using ScanFlowAWS.Application.DTOs.Responses.Rekognition;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces.Interface;
using ScanFlowAWS.Domain.Services;

namespace ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces
{
    public class CompareceFacesUseCase : ICompareceFaces
    {
        private readonly IRekognitionService _rekognition;

        public CompareceFacesUseCase(IRekognitionService rekognition)
        {
            _rekognition = rekognition;
        }

        public async Task<ResponseCompareceFacesJson> Execute(RequestCompareceFacesJson request)
        {

            ValidateUseCase(request);


            using var memoryStreamSource = new MemoryStream();
            using var memoryStreamTarget = new MemoryStream();

            await request.FileSource!.CopyToAsync(memoryStreamSource);
            await request.FileTarget!.CopyToAsync(memoryStreamTarget); 

            var result = _rekognition.CompareceFaces(memoryStreamSource.ToArray(), memoryStreamTarget.ToArray());

            var response = new ResponseCompareceFacesJson()
            {
                Similarity = result.Result.Similarity,
            };
            return response;
        }

        private void ValidateUseCase(RequestCompareceFacesJson request)
        {
            var validator = new CompareceFacesUseValidator();   
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }


        
    }
}
