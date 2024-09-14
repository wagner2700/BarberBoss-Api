using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.User
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseUserJson> Execute(UserRequestJson request);
    }
}
