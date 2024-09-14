using BarberBoss.Communication.Request;
using BarberBoss.Domain.Enums;
using Bogus;

namespace CommoTestsLibraries
{
    public static class BillRequestBuilder 
    {

        public static RequestBillJson Build()
        {
            var faker = new Faker();

            var request = new Faker<RequestBillJson>()
                .RuleFor(r => r.Data, f => f.Date.Past())
                .RuleFor(r => r.Descricao, f => f.Commerce.ProductDescription())
                .RuleFor(r => r.Valor, f => f.Finance.Amount())
                .RuleFor(r => r.TipoPagamento, f => f.PickRandom<PaymentType>()).Generate();

            return request;
                
                
        }
    }
}
