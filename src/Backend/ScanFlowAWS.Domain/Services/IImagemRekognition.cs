using ScanFlowAWS.Domain.ValueObjects;

namespace ScanFlowAWS.Domain.Services
{
    public interface IImagemRekognition
    {
        Task<List<ImageLabel>> AnalyzeImage(byte[] imageBytes);
    
    }
}
