using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Token;
using Moq;

namespace CommonTestsLibraries.Repositories
{
    public class TokenGeneratorBuilder
    {

        public static ITokenGenerator Build()
        {
            var mock = new Mock<ITokenGenerator>();

            mock.Setup(tokenGenerator => tokenGenerator.GenerateToken(It.IsAny<User>())).Returns("ggey2gedjshishfnsa");

            return mock.Object; 
        }
    }
}
