using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Bills
{
    public interface IBillReadOnlyRepository
    {
        Task<Bill?> GetById(long id);

        Task<List<Bill>> GetByMonth(DateOnly date);
    }
}
