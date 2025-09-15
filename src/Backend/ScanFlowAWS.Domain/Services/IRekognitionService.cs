using ScanFlowAWS.Domain.ValueObjects;

namespace ScanFlowAWS.Domain.Services
{
    public interface IRekognitionService
    {
        Task<List<ImageLabel>> AnalyzeImage(byte[] imageBytes);
        Task<List<ImageFace>> AnalyzeFace(byte[] imageBytes); 
    
    }
}
