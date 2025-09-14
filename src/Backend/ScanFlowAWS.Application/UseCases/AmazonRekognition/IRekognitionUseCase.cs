using ScanFlowAWS.Application.DTOs.Requests;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition
{
    public interface IRekognitionUseCase
    {
        Task<List<string>> Execute(RequestRekognition request);
    }
}
