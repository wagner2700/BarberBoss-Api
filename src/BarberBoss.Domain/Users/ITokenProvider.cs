namespace BarberBoss.Domain.Users
{
    public interface ITokenProvider
    {

        string TokenOnRequest();
    }
}
