using ScanFlowAWS.Domain.ValueObjects;

namespace ScanFlowAWS.Domain.Services
{
    public interface IRekognitionService
    {
        Task<List<ImageFace>> AnalyzeFace(byte[] imageBytes); 
    
    }
}
