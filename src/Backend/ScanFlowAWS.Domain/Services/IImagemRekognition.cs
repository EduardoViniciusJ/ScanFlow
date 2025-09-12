namespace ScanFlowAWS.Domain.Services
{
    public interface IImagemRekognition
    {
        Task<List<string>> AnalyzeImage(byte[] imageBytes);
    
    }
}
