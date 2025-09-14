using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Domain.ValueObjects;
using ScanFlowAWS.Infrastructure.Services;
using System.Linq;

namespace ScanFlowAWS.Infrastructure.Adapters
{
    public class ImageRekognitionAdapter : IImagemRekognition
    {
        private readonly RekognitionService _rekognitionService;

        public ImageRekognitionAdapter(RekognitionService rekognitionService)
        {
            _rekognitionService = rekognitionService;
        }

        public async Task<List<ImageLabel>> AnalyzeImage(byte[] imageBytes)
        {
            var labels = await _rekognitionService.DetectLabelsAsync(imageBytes);

            var result = new List<ImageLabel>();

            foreach (var l in labels)
            {
                var imageLabel = new ImageLabel(l.Name, (float)l.Confidence);
                result.Add(imageLabel);
            }
            return result;
        }
    }
}
