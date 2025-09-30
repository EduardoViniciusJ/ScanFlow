using System.Globalization;
using System.Threading.Tasks;

namespace ScanFlowAWS.API.Middlewares
{
    /// <summary> 
    /// Middleware responsável por configurar a cultura (CultureInfo) 
    /// da aplicação com base no cabeçalho Accept-Language da requisição HTTP. 
    /// </summary>
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var acceptLanguageHeader = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(acceptLanguageHeader))
            {
                var requestCulture = acceptLanguageHeader.Split(',').FirstOrDefault()?.Split(';').FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(requestCulture))
                {
                    var cultureInfo = new CultureInfo(requestCulture);

                    CultureInfo.CurrentCulture = cultureInfo;
                    CultureInfo.CurrentUICulture = cultureInfo;
                }
            }

            await _next(context);
        }
    }
}
