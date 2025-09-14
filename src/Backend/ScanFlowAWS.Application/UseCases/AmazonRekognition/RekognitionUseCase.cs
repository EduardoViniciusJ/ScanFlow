using Microsoft.AspNetCore.Http;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Domain.Services;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition
{
    public class RekognitionUseCase : IRekognitionUseCase
    {
        private readonly IImagemRekognition _imagemRekognition;

        public RekognitionUseCase(IImagemRekognition imagemRekognition)
        {
            _imagemRekognition = imagemRekognition;
        }

        public async Task<List<string>> Execute(RequestRekognition request)
        {
            ValidateUseCases(request);

            using var memoryStream  = new MemoryStream();
            await request.file.CopyToAsync(memoryStream);

            var result = await _imagemRekognition.AnalyzeImage(memoryStream.ToArray());

            return result;  
        }

        private void ValidateUseCases(RequestRekognition request)
        {
            var validator = new RekognitionUseValidator();
            var result = validator.Validate(request);
            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

    }
}
