using BarberBoss.Application.UseCases.User;
using BarberBoss.Exception;
using BarberBoss.Infraestructure.Exceptions;
using CommonTestsLibraries;
using CommonTestsLibraries.Mapper;
using CommonTestsLibraries.Repositories;
using FluentAssertions;

namespace UseCase.Test.Users.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var useCase = CreateUseCase();
            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Token.Should().NotBeNullOrWhiteSpace();
        }


        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            request.Name = string.Empty;

            var useCase = CreateUseCase();

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidatorException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NomeVazio));

        }


        private RegisterUserUseCase CreateUseCase()
        {

            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var registerUser = RegisterUserRepositoryBuilder.Build();
            var tokenGerator = TokenGeneratorBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var userReadOnly = new ReadOnlyBuilder().Build();

            return new RegisterUserUseCase(registerUser, mapper, unitOfWork, passwordEncripter, userReadOnly, tokenGerator);
        }
    }
}
