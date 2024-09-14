using BarberBoss.Communication.Response;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.User.Validator
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
    {

        public UpdateUserValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NomeVazio);

            RuleFor(request =>request.Email)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.EmailVazio)
                .EmailAddress()
                .When(request => string.IsNullOrEmpty(request.Email) == false).WithMessage(ResourceErrorMessages.EmailInvalido);
        }
    }
}
