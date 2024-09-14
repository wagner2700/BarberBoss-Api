using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Token
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
