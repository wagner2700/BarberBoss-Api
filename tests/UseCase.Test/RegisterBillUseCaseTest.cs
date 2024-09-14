using BarberBoss.Application.UseCases.Bill;
using BarberBoss.Domain.Entities;
using CommonTestsLibraries.Mapper;
using CommonTestsLibraries.Repositories;
using CommoTestsLibraries;
using FluentAssertions;

namespace WebApi.Tests.Bill
{
    public class RegisterBillUseCaseTest
    {

        [Fact]
        public async void Sucess()
        {
            var request = BillRequestBuilder.Build();
            var userLogged = UserBuilder.Build();
            var useCase = CreateUseCase(userLogged);

            var result = await useCase.Registrar(request);

            result.Valor.Should().BeGreaterThan(0);
            result.Descricao.Should().NotBeNullOrWhiteSpace();

        }



        private RegisterBillUseCase CreateUseCase(User user)
        {
            var writeOnly = BillWriteOnlyRepositoryBuilder.Build();
            var mapper = MapperBuilder.Build();
            var unitOfWord = UnitOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new RegisterBillUseCase(writeOnly, mapper, unitOfWord, loggedUser);
        }
    }
}
