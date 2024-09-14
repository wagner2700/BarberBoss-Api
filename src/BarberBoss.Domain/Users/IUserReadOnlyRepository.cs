using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Users
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> EmailExist(string email);
        Task<User?> GetByEmail(string email);

        Task<User?> GetById(long id);
    }
}
