namespace BarberBoss.Domain.Security
{
    public interface IPasswordEncrypter
    {
        string Encrypt(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
