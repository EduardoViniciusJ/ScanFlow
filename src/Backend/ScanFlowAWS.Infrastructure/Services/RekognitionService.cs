using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using ScanFlowAWS.Domain.ValueObjects;

namespace ScanFlowAWS.Infrastructure.Services
{
    /// <summary>
    /// Serviço responsável por encapsular chamadas da API da AWS Rekognition.
    /// Fornece métodos para detectar faces e comparar imagens.
    /// </summary>
    public class RekognitionService
    {
        private readonly IAmazonRekognition _rekognitionClient;

        /// <summary>
        /// Construtor do serviço <see cref="RekognitionService"/>.
        /// </summary>
        /// <param name="region">Região AWS onde o serviço Rekognition está hospedado.</param>
        public RekognitionService(string region)
        {
            _rekognitionClient = new AmazonRekognitionClient(Amazon.RegionEndpoint.GetBySystemName(region));
        }

        /// <summary>
        /// Detecta faces em uma imagem e retorna uma lista de emoções identificadas.
        /// </summary>
        /// <param name="imageBytes">Imagem em formato byte array a ser analisada.</param>
        /// <returns>Lista de <see cref="ImageFace"/> representando as emoções detectadas em cada face.</returns>
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

            // Para cada face detectada, pega a emoção com maior confiança
            foreach (var faceDetail in response.FaceDetails)
            {
                foreach (var emotion in faceDetail.Emotions.OrderByDescending(e => e.Confidence))
                {
                    // Adiciona cada emoção detectada ao ImageFace. 
                    result.Add(new ImageFace(emotion.Type, (float)emotion.Confidence));
                }
            }
            return result;
        }

        /// <summary>
        /// Compara duas imagens e retorna o percentual de similaridade entre as faces detectadas.
        /// </summary>
        /// <param name="imageBytesSource">Imagem de origem em byte array.</param>
        /// <param name="imageBytesTarget">Imagem alvo em byte array.</param>
        /// <returns>Objeto <see cref="CompareImage"/> com a similaridade calculada entre as imagens.</returns>
        public async Task<CompareImage> CompareceFaceAsync(byte[] imageBytesSource, byte[] imageBytesTarget)
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
                SimilarityThreshold = 90 // Considera apenas similaridades acima de 90%
            };

            var response = await _rekognitionClient.CompareFacesAsync(request);

            var result = new CompareImage();

            foreach (var f in response.FaceMatches)
            {
                result.Similarity = (float)f.Similarity; 
            }

            return result;
        }
    }
}

