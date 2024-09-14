using CommoTestsLibraries;
using FluentAssertions;
using BarberBoss.Application.UseCases.Bill;
using BarberBoss.Exception;
using BarberBoss.Application.UseCases.Bill.Validator;
using BarberBoss.Application.UseCases.User;
using Microsoft.IdentityModel.Tokens;
using CommonTestsLibraries.Mapper;
using CommonTestsLibraries.Repositories;
using BarberBoss.Domain.Entities;


namespace Validator.Tests.Bills.Register
{
    public class RegisterBillValidatorTest
    {

        [Fact]
        public void Sucess_Validator()
        {
            var validator = new BillValidator();

            var request = BillRequestBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().Be(true);
        }

        [Fact]
        public void Amoun_Equals_Zero()
        {
            var request = BillRequestBuilder.Build();
            var validator = new BillValidator();

            request.Valor = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().Be(false);
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.ValorMaiorQueZero));
        }


        //[Fact]
        //public void Sucess_UseCase()
        //{
        //    var loggedUser = 

        //}



        [Fact]
        public void Description_Is_Empty()
        {
            var request = BillRequestBuilder.Build();
            var validator = new BillValidator();

            request.Descricao = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().Be(false);
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.ValorMaiorQueZero));
        }

        private RegisterUserUseCase CreateUseCase(User user)
        {

            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userWrite = RegisterUserRepositoryBuilder.Build();
            var passwordEncripter  = PasswordEncripterBuilder.Build();
            var tokenGenerator = TokenGeneratorBuilder.Build();
            var userRead = new ReadOnlyBuilder();

            return new RegisterUserUseCase(userWrite, mapper, unitOfWork, passwordEncripter, null, tokenGenerator);
        }
    }
}
