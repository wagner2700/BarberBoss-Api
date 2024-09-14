using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Bills
{
    public interface IBillWriteOnlyRepository
    {
        Task RegisterBill(Bill fatura);
        Task Delete(User user , long id);
        

    }
}
