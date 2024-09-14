using BarberBoss.Domain.Entities;

namespace WebApi.Tests.Resources
{
    public class BillIdentityManager
    {

        private readonly BarberBoss.Domain.Entities.Bill _bill;

        public BillIdentityManager(BarberBoss.Domain.Entities.Bill bill)
        {
            _bill = bill;
        }

        public long GetBillId() => _bill.Id; 
    }
}
