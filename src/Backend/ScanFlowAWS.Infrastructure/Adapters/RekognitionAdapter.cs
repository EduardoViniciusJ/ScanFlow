using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Domain.ValueObjects;
using ScanFlowAWS.Infrastructure.Services;

namespace ScanFlowAWS.Infrastructure.Adapters
{
    public class RekognitionAdapter : IRekognitionService
    {
        private readonly RekognitionService _rekognitionService;

        public RekognitionAdapter(RekognitionService rekognitionService)
        {
            _rekognitionService = rekognitionService;
        }

        public async Task<List<ImageFace>> AnalyzeFace(byte[] imageBytes)
        {
            var images = await _rekognitionService.DetectFacesAsync(imageBytes);

            return images;
        }

        public async Task<CompareceFace> CompareceFaces(byte[] imageBytesSource, byte[] imageBytesTarget)
        {
            var compare = await _rekognitionService.CompareceFaceAsync(imageBytesSource, imageBytesTarget);

            return compare;
        }


    }
}
