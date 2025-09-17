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

            foreach (var faceDetail in response.FaceDetails)
            {
                foreach (var emotion in faceDetail.Emotions.OrderByDescending(e => e.Confidence))
                {
                    result.Add(new ImageFace(emotion.Type, (float)emotion.Confidence));
                }
            }
            return result;
        }

        public async Task<CompareceFace> CompareceFaceAsync(byte[] imageBytesSource, byte[] imageBytesTarget)
        {
            var request = new CompareFacesRequest()
            {
                SourceImage = new Image()
                {
                    Bytes = new MemoryStream(imageBytesSource)
                },
                TargetImage = new Image()
                {
                    Bytes = new MemoryStream(imageBytesTarget)
                },
                SimilarityThreshold = 90

            };

            var response = await _rekognitionClient.CompareFacesAsync(request);

            var result = new CompareceFace();

            foreach (var f in response.FaceMatches)
            {
                result.Similarity = (float)f.Similarity;
            }

            return result;
        }
    }
}
