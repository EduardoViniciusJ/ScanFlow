using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition
{
    public interface IRekognitionUseCase
    {
        Task<List<ResponseRekognition>> Execute(RequestRekognition request);
    }
}
