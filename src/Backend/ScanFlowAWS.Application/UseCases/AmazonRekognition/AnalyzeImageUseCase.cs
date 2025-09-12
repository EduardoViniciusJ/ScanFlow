using ScanFlowAWS.Domain.Services;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition
{
    public class AnalyzeImageUseCase
    {
        private readonly IImagemRekognition _imagemRekognition;

        public AnalyzeImageUseCase(IImagemRekognition imagemRekognition)
        {
            _imagemRekognition = imagemRekognition;
        }

        public async Task<List<string>> Execute(byte[] imageBytes)
        {
            return await _imagemRekognition.AnalyzeImage(imageBytes);
        }

    }
}
