using BarberBoss.Application.UseCases.User;
using BarberBoss.Domain.Entities;
using CommonTestsLibraries.Repositories;
using FluentAssertions;

namespace Validator.Tests.Users
{
    public class LoginUserTest
    {

        [Fact]
        public async Task Sucess()
        {
            
            var user =  UserBuilder.Build();
            var request =  RequestLoginUserJsonBuilder.Build();
            var useCase = CreateUseCase(user);

            var result = await useCase.Login(request);
            
            result.Name.Should().NotBeNullOrWhiteSpace();
            result.Token.Should().NotBeNullOrWhiteSpace();  
        }


        private IDoLoginUseCase CreateUseCase(User user)
        {

            var userReadOnly = new ReadOnlyBuilder().GetUserByEmail(user).Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var tokenGenerator = TokenGeneratorBuilder.Build();

            return new DoLoginUseCase(userReadOnly, passwordEncripter, tokenGenerator);
        }
    }
}
