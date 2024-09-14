using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Users;
using Moq;

namespace CommonTestsLibraries.Repositories
{
    public class LoggedUserBuilder
    {
        public static ILoggedUser Build(User user)
        {
            var mock = new Mock<ILoggedUser>(); 
            mock.Setup(user => user.Get()).ReturnsAsync(user);

            return mock.Object;
        }
            
        
    }
}
