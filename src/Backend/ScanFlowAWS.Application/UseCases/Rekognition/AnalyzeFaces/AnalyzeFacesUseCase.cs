using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces.Interfaces;
using ScanFlowAWS.Domain.Services;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition
{
    /// <summary>
    /// Caso de uso responsável por fazer a análise de rostos utilizando o serviço da Amazon Rekognition.
    /// Ele valida a requisição recebida, processa a imagem e retorna os resultados.
    /// </summary>
    public class AnalyzeFacesUseCase : IAnalyzeFacesUseCase
    {
        private readonly IRekognitionService _rekognition;
        private readonly ITranslatorJsonService _translator;

        /// <summary>
        /// Construtor da classe <see cref="AnalyzeFacesUseCase"/>.
        /// </summary>
        /// <param name="rekognition">Serviço responsável por consumir a API da Amazon Rekognition.</param>
        /// <param name="translator">Serviço responsável por traduzir os tipos de resultados para o idioma definido.</param>
        public AnalyzeFacesUseCase(IRekognitionService rekognition, ITranslatorJsonService translator)
        {
            _rekognition = rekognition;
            _translator = translator;
        }

        /// <summary>
        /// Executa a análise de rostos a partir do arquivo de imagem recebido na requisição.
        /// </summary>
        /// <param name="request">Objeto contendo o arquivo de imagem a ser processado.</param>
        /// <returns>Lista de <see cref="ResponseAnalyzeFacesJson"/> contendo os resultados da análise.</returns>
        /// <exception cref="ErrorOnValidationException">Lançada quando a requisição não passa pela validação.</exception>
        public async Task<List<ResponseAnalyzeFacesJson>> Execute(RequestAnalyzeFacesJson request)
        {
            ValidateUseCase(request);

            using var memoryStream = new MemoryStream();
            await request.file!.CopyToAsync(memoryStream);
            var result = await _rekognition.AnalyzeFace(memoryStream.ToArray());

            return result
                .Select(l => new ResponseAnalyzeFacesJson
                {
                    Type = _translator.Translate(l.Type),
                    Confidence = l.Confidence
                }).ToList();
        }

        /// <summary>
        /// Valida os dados da requisição utilizando as regras definidas no <see cref="AnalyzeFacesUseValidator"/>.
        /// </summary>
        /// <param name="request">Requisição a ser validada.</param>
        /// <exception cref="ErrorOnValidationException">Lançada caso a validação falhe.</exception>
        private void ValidateUseCase(RequestAnalyzeFacesJson request)
        {
            var validator = new AnalyzeFacesUseValidator();
            var result = validator.Validate(request);
            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
