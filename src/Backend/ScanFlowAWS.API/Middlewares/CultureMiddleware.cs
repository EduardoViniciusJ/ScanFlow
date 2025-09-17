using System.Globalization;
using System.Threading.Tasks;

namespace ScanFlowAWS.API.Middlewares
{
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
