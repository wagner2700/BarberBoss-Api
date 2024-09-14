using BarberBoss.Communication.Request;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.UseCases.Bill
{
    public interface IUpdateBillUseCase
    {
        Task Update(long id , RequestBillJson request);
        
    }
}
