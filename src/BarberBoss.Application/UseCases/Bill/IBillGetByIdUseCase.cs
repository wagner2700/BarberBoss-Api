using BarberBoss.Communication.Response;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.UseCases.Bill
{
    public interface IBillGetByIdUseCase
    {
        Task<ResponseBillJson> GetById(long id);
    }
}
