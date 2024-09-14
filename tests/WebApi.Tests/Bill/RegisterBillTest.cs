using BarberBoss.Application.UseCases.Bill;
using BarberBoss.Domain.Entities;
using CommonTestsLibraries;
using CommonTestsLibraries.Mapper;
using CommonTestsLibraries.Repositories;
using CommoTestsLibraries;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Tests.Bill
{
    public  class RegisterBillTest : CustomClassFixture
    {
        private readonly HttpClient _httpClient;

        private const string METHOD = "api/fatura";

        public RegisterBillTest(CustomWebApplicatioFactory factory) : base(factory)
        {

        }
        


        [Fact]
        public async Task Sucess()
        {
            var request = BillRequestBuilder.Build();
            var user = UserBuilder.Build();

            var result = await DoPost(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = result.Content.ReadAsStream();
            var response = JsonDocument.Parse(body);

            response.RootElement.GetProperty("descricao").GetString().Should().Be(request.Descricao);
            
        }


        private RegisterBillUseCase CreateUseCase(User user)
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var writeFaturaRepository = BillWriteOnlyRepositoryBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);


            return new RegisterBillUseCase(writeFaturaRepository, mapper, unitOfWork, loggedUser);
        }
    }
}
