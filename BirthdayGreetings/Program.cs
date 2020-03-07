using BirthdayGreeting.Business;
using BirthdayGreetings.DataAccess;
using BirthdayGreetings.Interfaces;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BirthdayGreetings
{
    class Program
    {
        private static IConfiguration _configuration;
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            BuildConfiguration();
            InjectDependencies();

            var test = _serviceProvider.GetRequiredService<IMessageSenderBusiness>();

            List<DataAccess.Person> peopleOnBirthday = CreateFakePeople(10);
            var contactsAndMessage = new Dictionary<string, string>();

            peopleOnBirthday.ForEach(p =>
            {
                var message = new StringBuilder();

                message.AppendLine("Subject: Happy birthday!");
                message.AppendLine();
                message.AppendLine($"Happy birthday, dear { p.FirstName }!");

                contactsAndMessage.Add(p.Email, message.ToString());
            });

            test.SendMessage(contactsAndMessage);
        }

        private static void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", false, true);

            _configuration = builder.Build();
        }

        private static void InjectDependencies()
        {
            var services = new ServiceCollection()
                .AddDbContext<BirthdayGreetingContext>(options => options.UseSqlServer(_configuration.GetConnectionString("BirthDayGreetingsConnection")))
                .AddScoped<IMessageSenderBusiness, MailSenderBusiness>()
                .AddLogging();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected static List<DataAccess.Person> CreateFakePeople(int howManyPeople)
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
