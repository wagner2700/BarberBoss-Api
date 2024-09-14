using BarberBoss.Domain.Security;
using Moq;

namespace CommonTestsLibraries.Repositories
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncrypter Build()
        {
            var mock = new Mock<IPasswordEncrypter>();
            mock.Setup(passwordEncripter => passwordEncripter.Encrypt(It.IsAny<string>())).Returns("eswadsdfsafadsfdafd");

            return mock.Object;
        }
    }
}
