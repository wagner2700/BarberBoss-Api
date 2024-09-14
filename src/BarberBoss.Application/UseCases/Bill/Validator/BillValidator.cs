using BarberBoss.Communication.Request;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Bill.Validator
{
    public class BillValidator : AbstractValidator<RequestBillJson>
    {

        public BillValidator()
        {
            RuleFor(bill => bill.Valor).GreaterThan(0).WithMessage(ResourceErrorMessages.ValorMaiorQueZero);
            RuleFor(bill => bill.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.DataNaoFututo);
            RuleFor(bill => bill.TipoPagamento).IsInEnum().WithMessage(ResourceErrorMessages.PagamentoInválido);
            RuleFor(bill => bill.Tags).ForEach(x => x.IsInEnum());
        }
    }
}
