
using BarberBoss.Domain.Enums;

namespace BarberBoss.Communication.Response
{
    public  class ResponseBillJson
    {
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public IList<Tag> Tags { get; set; } = [];
    }
}
