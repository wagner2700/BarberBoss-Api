using BarberBoss.Communication.Request;
using Bogus;

namespace CommonTestsLibraries
{
    public class RequestRegisterUserJsonBuilder
    {

        public static UserRequestJson Build()
        {
            return new Faker<UserRequestJson>()
                .RuleFor(user => user.Name, faker => faker.Person.FirstName)
                .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Email))
                .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "Wg!12", length: 10));

        }
    }
}
