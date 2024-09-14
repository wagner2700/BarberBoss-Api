using BarberBoss.Exception;
using CommonTestsLibraries;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Tests.InLineData;

namespace WebApi.Tests.Users.Register
{
    public class RegisterUserTest : IClassFixture<CustomWebApplicatioFactory>
    {
        private readonly HttpClient _httpClient;

        private const string METHOD = "api/user";

        public RegisterUserTest(CustomWebApplicatioFactory webHttp)
        {
            _httpClient = webHttp.CreateClient();
        }

        [Fact]  
        public async Task Sucess()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await _httpClient.PostAsJsonAsync(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("name").GetString().Should().Be(request.Name);
            response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Name_Empty(string language)
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("en"));

            var result = await _httpClient.PostAsJsonAsync(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);
            var error = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NomeVazio", new System.Globalization.CultureInfo("en"));

            error.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
