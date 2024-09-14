using BarberBoss.Communication.Request;
using Bogus;

namespace CommonTestsLibraries.Repositories
{
    public class RequestLoginUserJsonBuilder
    {

        public static RequestLoginJson Build()
        {
            return new Faker<RequestLoginJson>()
                .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Email))
                .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "Wg!12", length: 10));
        }
    }
}
