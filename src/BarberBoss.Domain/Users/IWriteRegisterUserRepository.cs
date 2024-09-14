using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Users
{
    public interface IWriteRegisterUserRepository
    {
        Task Execute(User user);
        void Update(User user);
    }
}
