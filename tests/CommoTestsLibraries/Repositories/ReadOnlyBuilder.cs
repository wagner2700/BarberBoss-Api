using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Users;
using Moq;

namespace CommonTestsLibraries.Repositories
{
    public class ReadOnlyBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;


        public ReadOnlyBuilder()
        {
            _repository = new Mock<IUserReadOnlyRepository>();

            
        }

        public ReadOnlyBuilder GetUserByEmail(User user)
        {
            _repository.Setup(repo => repo.GetByEmail(user.Email)).ReturnsAsync(user);
            return this;
        }


        public IUserReadOnlyRepository Build() => _repository.Object;
    }
}
