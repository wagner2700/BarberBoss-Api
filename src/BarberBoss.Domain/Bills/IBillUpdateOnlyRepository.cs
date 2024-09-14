using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Bills
{
    public interface IBillUpdateOnlyRepository
    {
        void Update(Bill fatura);
        Task<Bill?> GetById(User user ,long id);
    }
}
