using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Domain.ValueObjects;
using ScanFlowAWS.Infrastructure.Services;

namespace ScanFlowAWS.Infrastructure.Adapters
{
    /// <summary>
    /// Adapter responsável por integrar a aplicação com o serviço AWS Rekognition.
    /// Faz a ponte entre o serviço interno <see cref="RekognitionService"/> e os DTOs/ValueObjects da aplicação.
    /// </summary>
    public class RekognitionAdapter : IRekognitionService
    {
        private readonly RekognitionService _rekognitionService;

        /// <summary>
        /// Construtor do adapter <see cref="RekognitionAdapter"/>.
        /// </summary>
        /// <param name="rekognitionService">Serviço usado para analisar e comparar imagens com a AWS Rekognition.</param>
        public RekognitionAdapter(RekognitionService rekognitionService)
        {
            _rekognitionService = rekognitionService;
        }

        /// <summary>
        /// Analisa as faces presentes em uma imagem.
        /// </summary>
        /// <param name="imageBytes">Imagem em formato byte array a ser analisada.</param>
        /// <returns>Lista de <see cref="ImageFace"/> com informações das faces detectadas.</returns>
        public async Task<List<ImageFace>> AnalyzeFace(byte[] imageBytes)
        {
            var images = await _rekognitionService.DetectFacesAsync(imageBytes);
            return images;
        }

        /// <summary>
        /// Compara duas imagens e determina a similaridade das faces.
        /// </summary>
        /// <param name="imageBytesSource">Imagem de origem.</param>
        /// <param name="imageBytesTarget">Imagem alvo.</param>
        /// <returns>Objeto <see cref="CompareImages"/> contendo o resultado da comparação.</returns>
        public async Task<CompareImage> CompareImages(byte[] imageBytesSource, byte[] imageBytesTarget)
        {
            var compare = await _rekognitionService.CompareceFaceAsync(imageBytesSource, imageBytesTarget);
            return compare;
        }
    }
}

