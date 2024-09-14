using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CommonTestsLibraries
{
    public class CustomClassFixture : IClassFixture<CustomWebApplicatioFactory>
    {
        protected HttpClient _httpClient;

        public CustomClassFixture(CustomWebApplicatioFactory factory)
        {
            _httpClient = factory.CreateClient();
        }


        protected async Task<HttpResponseMessage> DoPost(string requestUri , object request  , string token = "" , string culture = "pt-BR")
        {

            AuthorizeRequest(token);
            ChangeRequestCulture(culture);

            return  await _httpClient.PostAsJsonAsync(requestUri, request);
        }


        private void AuthorizeRequest( string token)
        {
            if(!string.IsNullOrWhiteSpace( token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            
        }

        private void ChangeRequestCulture(string culture)
        {
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));
        }
    }
}
