using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Infrastructure.TranslatorJson;

namespace ScanFlowAWS.Infrastructure.Services
{
    /// <summary>
    /// Serviço responsável por traduzir o JSON de emoções retornados pelo Rekognition
    /// para mensagens definidas em <see cref="ResourceMessageTranslation"/>.
    /// </summary>
    public class TranslatorJsonService : ITranslatorJsonService
    {
        // Dicionário que mapeia cada tipo de emoção para sua mensagem traduzida
        private readonly Dictionary<string, string> _translations = new()
        {
            { "CALM", ResourceMessageTranslation.MESSAGE_CALM },
            { "SAD", ResourceMessageTranslation.MESSAGE_SAD },
            { "ANGRY", ResourceMessageTranslation.MESSAGE_ANGRY },
            { "CONFUSED", ResourceMessageTranslation.MESSAGE_CONFUSED },
            { "DISGUSTED", ResourceMessageTranslation.MESSAGE_DISGUSTED },
            { "SURPRISED", ResourceMessageTranslation.MESSAGE_SURPRISED },
            { "HAPPY", ResourceMessageTranslation.MESSAGE_HAPPY },
            { "FEAR", ResourceMessageTranslation.MESSAGE_FEAR }
        };

        /// <summary>
        /// Método para traduzir.
        /// </summary>
        /// <param name="key">Chave da emoção retornado pelo Rekognition.</param>
        /// <returns>
        /// Mensagem traduzida correspondente à emoção.
        /// Se não houver tradução, retorna a própria chave.
        /// </returns>
        public string Translate(string key)
        {
            // Verifica se existe uma tradução para a chave fornecida
            if (_translations.ContainsKey(key))
            {
                return _translations[key]; // Retorna a tradução
            }

            // Caso não exista tradução, retorna a própria chave
            return key;
        }
    }
}
