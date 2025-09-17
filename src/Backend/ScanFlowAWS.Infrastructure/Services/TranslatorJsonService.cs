using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Infrastructure.TranslatorJson;

namespace ScanFlowAWS.Infrastructure.Services
{
    public class TranslatorJsonService : ITranslatorJsonService
    {
        private readonly Dictionary<string, string> _translations = new()
        {
        { "CALM",ResourceMessageTranslation.MESSAGE_CALM},
        { "SAD", ResourceMessageTranslation.MESSAGE_SAD },
        { "ANGRY", ResourceMessageTranslation.MESSAGE_ANGRY },
        { "CONFUSED", ResourceMessageTranslation.MESSAGE_CONFUSED },
        { "DISGUSTED", ResourceMessageTranslation.MESSAGE_DISGUSTED },
        { "SURPRISED", ResourceMessageTranslation.MESSAGE_SURPRISED },
        { "HAPPY", ResourceMessageTranslation.MESSAGE_HAPPY },
        { "FEAR", ResourceMessageTranslation.MESSAGE_FEAR }
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
