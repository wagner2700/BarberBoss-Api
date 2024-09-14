using BarberBoss.Domain.Bills;
using Moq;

namespace CommonTestsLibraries.Repositories
{
    public class BillWriteOnlyRepositoryBuilder
    {
        public static IBillWriteOnlyRepository Build()
        {
            var mock = new Mock<IBillWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
