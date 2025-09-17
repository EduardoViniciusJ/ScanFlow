using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Domain.ValueObjects;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces.Interfaces
{
    public interface IAnalyzeFacesUseCase
    {
        Task<List<ResponseAnalyzeFacesJson>> Execute(RequestAnalyzeFacesJson request);
    }
}
