using BarberBoss.Domain.Security;

namespace BarberBoss.Infraestructure.Security
{
    public class BcryptPasswordEncryptor : IPasswordEncrypter
    {
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        public bool VerifyPassword(string password , string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
