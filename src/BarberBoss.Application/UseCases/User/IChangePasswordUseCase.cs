using BarberBoss.Communication.Request;

namespace BarberBoss.Application.UseCases.User
{
    public interface IChangePasswordUseCase
    {
        Task Execute(ChangePasswordRequest request);
    }
}
