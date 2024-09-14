using BarberBoss.Exception;
using FluentValidation;
using FluentValidation.Validators;
using System.Resources;
using System.Text.RegularExpressions;

namespace BarberBoss.Application.UseCases.User.Validator
{
    public class PasswordValidator<T> : PropertyValidator< T ,  string>
    {
        public override string Name => "PasswordValidator";


        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                context.MessageFormatter.AppendArgument("errormessage", ResourceErrorMessages.SenhaVazia);
                return false;
            }

            if(password.Length < 6)
            {
                context.MessageFormatter.AppendArgument("errormessage", ResourceErrorMessages.SenhainvalidaMaiorSeisCaracter);
                return false;
            }

            if(!Regex.IsMatch(password, @"[A-Z]"))
            {
                context.MessageFormatter.AppendArgument("errormessage", ResourceErrorMessages.SenhainvalidaLetraMaiuscula);
                return false;
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                context.MessageFormatter.AppendArgument("errormessage", ResourceErrorMessages.SenhainvalidaLetraMaiuscula);
                return false;
            }

            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                context.MessageFormatter.AppendArgument("errormessage", ResourceErrorMessages.SenhainvalidaCaracterNumerico);
                return false;
            }
            if (!Regex.IsMatch(password, @"[\\@\\!\\?\\*\\.]")) // Caracteres especiais
            {
                context.MessageFormatter.AppendArgument("errorMessage", ResourceErrorMessages.SenhainvalidaCaracterEspecial);
                return false;
            }

            return true;
        }
    }
}
