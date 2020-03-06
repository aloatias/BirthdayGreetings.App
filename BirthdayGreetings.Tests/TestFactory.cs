using BirthdayGreetings.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

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
    }
}
