using BarberBoss.Communication.Request;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.User.Validator
{
    public class UserValidator :AbstractValidator<UserRequestJson>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NomeVazio);
            RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EmailVazio)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EmailInvalido);

            RuleFor(user => user.Password).SetValidator(new PasswordValidator<UserRequestJson>());
            
        }
    }
}
