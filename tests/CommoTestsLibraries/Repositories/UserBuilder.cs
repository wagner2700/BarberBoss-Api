using BarberBoss.Communication.Request;
using BarberBoss.Domain.Entities;
using Bogus;

namespace CommonTestsLibraries.Repositories
{
    public class UserBuilder
    {

        public static User Build()
        {
            return new Faker<User>()
                .RuleFor(user => user.Id, _ => 1)
                .RuleFor(user => user.Name, faker => faker.Person.FirstName)
                .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name))
                .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "Wg!12"))
                .RuleFor(user => user.UserIdentifier, _ => Guid.NewGuid());




        }
    }
}
