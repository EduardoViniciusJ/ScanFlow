using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using ScanFlowAWS.Domain.ValueObjects;

namespace ScanFlowAWS.Infrastructure.Services
{
    public class RekognitionService
    {
        private readonly IAmazonRekognition _rekognitionClient;

        public RekognitionService(string region)
        {
            _rekognitionClient = new AmazonRekognitionClient(Amazon.RegionEndpoint.GetBySystemName(region));
        }

        public async Task<List<ImageLabel>> DetectLabelsAsync(byte[] imageBytes)
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

            var result = new List<ImageLabel>();

            foreach (var l in response.Labels)
            {
                var imageLabel = new ImageLabel(l.Name, (float)l.Confidence);
                result.Add(imageLabel);
            }

            return result;
        }

        public async Task<List<ImageFace>> DetectFacesAsync(byte[] imageBytes)
        {
            var request = new DetectFacesRequest
            {
                Image = new Image()
                {
                    Bytes = new MemoryStream(imageBytes)
                },
                Attributes = new List<string> { "ALL" }

            };

            var response = await _rekognitionClient.DetectFacesAsync(request);

            var result = new List<ImageFace>();

            foreach(var faceDetail in response.FaceDetails)
            {
                foreach (var emotion in faceDetail.Emotions.OrderByDescending(e => e.Confidence))
                {
                    result.Add(new ImageFace(emotion.Type, (float)emotion.Confidence));
                }
            }

            return result;
        }


    }
}
