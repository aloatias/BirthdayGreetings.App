using BirthdayGreetings.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace BirthdayGreetings.Person.Tests
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
    }
}
