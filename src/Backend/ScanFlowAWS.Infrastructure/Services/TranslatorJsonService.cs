using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Infrastructure.Localization;
using System.Text.Json;

namespace ScanFlowAWS.Infrastructure.Services
{
    public class TranslatorJsonService : ITranslatorJsonService
    {
        private readonly Dictionary<string, string> _translations = new()
        {
        { "CALM", ResourceMessageTranslations.MESSAGE_CALM },
        { "SAD", ResourceMessageTranslations.MESSAGE_SAD },
        { "ANGRY", ResourceMessageTranslations.MESSAGE_ANGRY },
        { "CONFUSED", ResourceMessageTranslations.MESSAGE_CONFUSED },
        { "DISGUSTED", ResourceMessageTranslations.MESSAGE_DISGUSTED },
        { "SURPRISED", ResourceMessageTranslations.MESSAGE_SURPRISED },
        { "HAPPY", ResourceMessageTranslations.MESSAGE_HAPPY },
        { "FEAR", ResourceMessageTranslations.MESSAGE_FEAR }
        };


        public string Translate(string key)
        {
            if (_translations.ContainsKey(key))
            {
                return _translations[key];
            }
            return key;
        }

    }
}
