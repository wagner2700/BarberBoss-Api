using BarberBoss.Domain.Enums;

namespace BarberBoss.Domain.Entities
{
    public class Bill
    {
        public long Id { get; set; }    
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public PaymentType PaymentType { get; set; }

        public ICollection<Domain.Entities.Tag> Tags { get; set; } = [];

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
