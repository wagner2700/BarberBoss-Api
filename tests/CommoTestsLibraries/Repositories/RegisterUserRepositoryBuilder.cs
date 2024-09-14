using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Users;
using Moq;

namespace CommonTestsLibraries.Repositories
{
    public class RegisterUserRepositoryBuilder
    {
        public static IWriteRegisterUserRepository Build()
        {
            var mock = new Mock<IWriteRegisterUserRepository>();

            return mock.Object;
        }

      
    }
}
