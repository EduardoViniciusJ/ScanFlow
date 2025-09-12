using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Infrastructure.Services;

namespace ScanFlowAWS.Infrastructure.Adapters
{
    public class ImageRekognitionAdapter : IImagemRekognition
    {
        private readonly RekognitionService _rekognitionService;    

        public ImageRekognitionAdapter(RekognitionService rekognitionService)
        {
            _rekognitionService = rekognitionService;
        }

        public async Task<List<string>> AnalyzeImage(byte[] imageBytes)
        {

            var labels = await _rekognitionService.DetectLabelsAsync(imageBytes);
            return labels.Select(l => $"{l.Name} ({l.Confidence}%)").ToList();
        }
    }
}
