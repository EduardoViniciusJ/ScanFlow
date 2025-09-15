using Microsoft.AspNetCore.Http;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.Interface;
using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Domain.ValueObjects;
using System.Reflection.Emit;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition
{
    public class RekognitionUseCase : IRekognitionUseCase
    {
        private readonly IRekognitionService _imagemRekognition;

        public RekognitionUseCase(IRekognitionService imagemRekognition)
        {
            _imagemRekognition = imagemRekognition;
        }

        public async Task<List<ResponseRekognitionJson>> ExecuteFaces(RequestRekognitionJson request)
        {
            ValidateUseCases(request);

            using var memoryStream = new MemoryStream();
            await request.file.CopyToAsync(memoryStream);
            var result = await _imagemRekognition.AnalyzeFace(memoryStream.ToArray());

            return result
        .Select(l => new ResponseRekognitionJson
        {
            Type = l.Type,
            Confidence = l.Confidence
        }).ToList();

        }

        private void ValidateUseCases(RequestRekognitionJson request)
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
