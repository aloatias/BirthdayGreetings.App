using BirthdayGreetings.DataAccess;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;

namespace BirthdayGreetings.Tests
{
    public class TestFactory
    {
        protected BirthdayGreetingContext CreateContext()
        {
            IServiceCollection servicesCollection = new ServiceCollection()
                .AddDbContext<BirthdayGreetingContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            ServiceProvider build = servicesCollection.BuildServiceProvider();

            return build.GetRequiredService<BirthdayGreetingContext>();
        }

        protected Mock<T> MockMe<T>() where T : class
        {
            return new Mock<T>();
        }

        protected List<DataAccess.Person> CreateFakePeople(int howManyPeople)
        {
            var fakePeople = new Faker<DataAccess.Person>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.DateOfBirth, f => f.Date.Between(DateTime.Now.AddYears(-35), DateTime.Now))
                .RuleFor(p => p.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName));

            var people = fakePeople.Generate(howManyPeople);

            return people;
        }
    }
}
