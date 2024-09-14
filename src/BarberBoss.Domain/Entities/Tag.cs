using BarberBoss.Domain.Enums;

namespace BarberBoss.Domain.Entities
{
    public class Tag
    {
        public int id { get; set; }
        public Domain.Enums.Tag TagValue {  get; set; }
        
        public long BillId { get; set; }
        public Bill? Bill { get; set; } = default;

    }
}
