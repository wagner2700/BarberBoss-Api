namespace BarberBoss.Application.UseCases.Bill
{
    public interface IDeleteBillUseCase
    {
        Task Execute(long id);
    }
}
