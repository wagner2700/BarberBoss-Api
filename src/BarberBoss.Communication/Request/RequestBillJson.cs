using BarberBoss.Domain.Enums;

namespace BarberBoss.Communication.Request
{
    public class RequestBillJson
    {
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public PaymentType TipoPagamento { get; set; }
        public List<Domain.Enums.Tag> Tags { get; set; } = [];
    }
}
