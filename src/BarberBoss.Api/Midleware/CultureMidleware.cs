using System.Globalization;

namespace BarberBoss.Api.Midleware
{
    public class CultureMidleware
    {
        private readonly RequestDelegate _requestDelegate;

        public CultureMidleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList(); // Pegar todos os idiomas

            var requestedCulture = httpContext.Request.Headers.AcceptLanguage.FirstOrDefault();

            // Idioma default
            var cultureInfo = new CultureInfo("pt-BR");



            if (string.IsNullOrWhiteSpace(requestedCulture) == false
                && supportedLanguages.Exists(l => l.Name.Equals(requestedCulture))
                )
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _requestDelegate(httpContext);
        }
    }
}
