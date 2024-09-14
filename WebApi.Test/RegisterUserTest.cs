

using CommonTestsLibraries;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace WebApi.Test
{
    public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
    {
        private const string METHOD = "api/user";
        private readonly HttpClient _httpClient;


        public RegisterUserTest(WebApplicationFactory<Program>webApplication)
        {
            _httpClient = webApplication.CreateClient();
        }

        [Fact]
        public async Task Sucess()
        {
     

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await _httpClient.PostAsJsonAsync(METHOD , request);

            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
