using BarberBoss.Communication.Request;
using FluentValidation;

namespace BarberBoss.Application.UseCases.User.Validator
{
    public class ChangePasswordValidator: AbstractValidator<ChangePasswordRequest>
    {

        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<ChangePasswordRequest>());
        }
    }
}
