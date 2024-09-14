using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.User;
    public interface IDoLoginUseCase
    {
        Task<ResponseUserJson> Login(RequestLoginJson request);
    }

