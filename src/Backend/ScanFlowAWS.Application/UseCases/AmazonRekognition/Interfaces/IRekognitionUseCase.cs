using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Domain.ValueObjects;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition.Interface
{
    public interface IRekognitionUseCase
    {
        Task<List<ResponseRekognitionJson>> ExecuteLabels(RequestRekognitionJson request);
        Task<List<ResponseRekognitionJson>> ExecuteFaces(RequestRekognitionJson request);    
    }
}
