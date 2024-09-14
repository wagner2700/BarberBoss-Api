using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Users
{
    public interface ILoggedUser
    {
        Task<User?> Get();
    }
}
