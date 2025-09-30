using ScanFlowAWS.Application.DTOs.Requests.Rekognition;
using ScanFlowAWS.Application.DTOs.Responses.Rekognition;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces.Interface;
using ScanFlowAWS.Domain.Services;

namespace ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces
{
    /// <summary>
    /// Caso de uso responsável por comparar duas imagens e determinar se as imagens são iguais.
    /// </summary>
    public class CompareceFacesUseCase : ICompareceFaces
    {
        private readonly IRekognitionService _rekognition;

        /// <summary>
        /// Construtor da classe <see cref="CompareceFacesUseCase"/>.
        /// </summary>
        /// <param name="rekognition">Serviço responsável pela comunicação com a API da Amazon Rekognition.</param>
        public CompareceFacesUseCase(IRekognitionService rekognition)
        {
            _rekognition = rekognition;
        }

        /// <summary>
        /// Executa a comparação das faces das duas imagens enviadas.
        /// </summary>
        /// <param name="request">Objeto contendo as duas imagens a serem comparadas.</param>
        /// <returns>Objeto<see cref="ResponseCompareceFacesJson"/> com a similaridade entre as faces.</returns>
        /// <exception cref="ErrorOnValidationException">Lançada quando a validação das imagens falha.</exception>
        public async Task<ResponseCompareceFacesJson> Execute(RequestCompareceFacesJson request)
        {
            ValidateUseCase(request);

            using var memoryStreamSource = new MemoryStream();
            using var memoryStreamTarget = new MemoryStream();

            await request.FileSource!.CopyToAsync(memoryStreamSource);
            await request.FileTarget!.CopyToAsync(memoryStreamTarget);

            var result = _rekognition.CompareceFaces(memoryStreamSource.ToArray(), memoryStreamTarget.ToArray());

            var response = new ResponseCompareceFacesJson()
            {
                Similarity = result.Result.Similarity,
            };
            return response;
        }

        /// <summary>
        /// Valida os arquivos de entrada utilizando <see cref="CompareceFacesUseValidator"/>.
        /// </summary>
        /// <param name="request">Requisição contendo os arquivos de imagem.</param>
        /// <exception cref="ErrorOnValidationException">Lançada se algum dos arquivos ou regras de validação estiverem incorretos.</exception>
        private void ValidateUseCase(RequestCompareceFacesJson request)
        {
            var validator = new CompareceFacesUseValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}

