using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.User
{
    public interface IGetUserProfileUseCase
    {
        Task<ResponseUserProfileJson> Execute();
    }
}
