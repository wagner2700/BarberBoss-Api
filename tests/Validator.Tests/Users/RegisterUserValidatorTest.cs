using BarberBoss.Application.UseCases.User.Validator;
using BarberBoss.Exception;
using CommonTestsLibraries;
using FluentAssertions;

namespace Validator.Tests.Users
{
    public class RegisterUserValidatorTest
    {

        [Fact]
        public void Sucess()
        {
            var validator = new UserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
            
        }

        [Fact]
        public void Error_Name_Invalid()
        {
            var validator = new UserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var result = validator.Validate(request);

            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            result.Errors.Should().HaveCount(1).And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.NomeVazio));
        }


        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData("wagner.com.br")]
        public void Error_Email_Invalid(string email)
        {
            var validator = new UserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = email;

            var result = validator.Validate(request);

            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
            result.Errors.Should().HaveCount(1);
        }
    }
}
