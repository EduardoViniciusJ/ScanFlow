using Microsoft.AspNetCore.Http;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces.Interfaces;
using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Domain.ValueObjects;
using System.Reflection.Emit;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition
{
    public class AnalyzeFacesUseCase : IAnalyzeFacesUseCase
    {
        private readonly IRekognitionService _rekognition;

        public AnalyzeFacesUseCase(IRekognitionService rekognition)
        {
            _rekognition = rekognition;
        }

        public async Task<List<ResponseAnalyzeFacesJson>> Execute(RequestAnalyzeFacesJson request)
        {
            ValidateUseCase(request);

            using var memoryStream = new MemoryStream();
            await request.file!.CopyToAsync(memoryStream);
            var result = await _rekognition.AnalyzeFace(memoryStream.ToArray());

            return result
        .Select(l => new ResponseAnalyzeFacesJson
        {
            Type = l.Type,
            Confidence = l.Confidence
        }).ToList();

        }

        private void ValidateUseCase(RequestAnalyzeFacesJson request)
        {
            var validator = new AnalyzeFacesUseValidator();
            var result = validator.Validate(request);
            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

    }
}
