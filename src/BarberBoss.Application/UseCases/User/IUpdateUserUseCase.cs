using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.User
{
    public interface IUpdateUserUseCase
    {

        Task Execute(RequestUpdateUserJson request);
    }
}
