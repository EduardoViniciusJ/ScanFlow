using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace ScanFlowAWS.Infrastructure.Services
{
    public class RekognitionService
    {
        private readonly IAmazonRekognition _rekognitionClient;

        public RekognitionService(string region)
        {
            _rekognitionClient = new AmazonRekognitionClient(Amazon.RegionEndpoint.GetBySystemName(region));
        }

        public async Task<List<Label>> DetectLabelsAsync(byte[] imageBytes)
        {
            var request = new DetectLabelsRequest
            {
                Image = new Image
                {
                    Bytes = new MemoryStream(imageBytes)
                },
                MaxLabels = 10,
                MinConfidence = 75F
            };

            var response = await _rekognitionClient.DetectLabelsAsync(request);

            return response.Labels;
        }

    }
}
