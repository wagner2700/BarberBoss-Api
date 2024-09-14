using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.Bill
{
    public interface IRegisterBillUseCase
    {

        Task<ResponseBillJson> Registrar(RequestBillJson request);
    }
}
