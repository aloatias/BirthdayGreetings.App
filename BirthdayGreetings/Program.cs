using BirthdayGreetings.Business;
using BirthdayGreetings.DataAccess;
using BirthdayGreetings.Interfaces;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            var context = _serviceProvider.GetRequiredService<BirthdayGreetingContext>();

            var peopleOnBirthday = new List<DataAccess.Person>();
            var peopleToNotify = new List<DataAccess.Person>();

            if (!context.Person.Any())
            {
                peopleOnBirthday = CreateFakePeople(10);
                peopleToNotify = CreateFakePeople(20);
            }
            else
            {
                var peopleBusiness = _serviceProvider.GetRequiredService<IPeopleBusiness>();

                peopleOnBirthday = peopleBusiness.GetBirthDayPeople();
                peopleToNotify = peopleBusiness.GetAllPeople();
            }

            // Parse messages
            var messageParserBusiness = _serviceProvider.GetRequiredService<IMessageParserBusiness>();
            var personalBirthdayWishMessages = messageParserBusiness.CreatePersonalBirthDayWish(peopleOnBirthday);

            var individualBirthdayReminderMessages = messageParserBusiness.CreateIndividualBirthDayReminder(peopleOnBirthday, peopleToNotify);

            var generalBirthdayReminderMessages = messageParserBusiness.CreateGeneralBirthDayReminder(peopleOnBirthday, peopleToNotify);

            // Send messages
            var messageSenderBusiness = _serviceProvider.GetRequiredService<IMessageSenderBusiness>();

            // Parse personal birthday wish
            var birthdayWishContactAndMessage = new Dictionary<string, string>();
            foreach (var contact in personalBirthdayWishMessages)
            {
                birthdayWishContactAndMessage.Add(contact.Key.Email, contact.Value);
            }

            messageSenderBusiness.SendMessage(birthdayWishContactAndMessage);

            // Parse individual birthday reminder
            var individualBirthdayReminderContactAndMessage = new List<Tuple<string, string>>();
            foreach (var contact in individualBirthdayReminderMessages)
            {
                individualBirthdayReminderContactAndMessage.Add(Tuple.Create(contact.Item1.Email, contact.Item2));
            }

            messageSenderBusiness.SendMessage(individualBirthdayReminderContactAndMessage);

            // Parse general birthday reminder
            var generalBirthdayReminderContactAndMessage = new Dictionary<string, string>();
            foreach (var contact in generalBirthdayReminderMessages)
            {
                generalBirthdayReminderContactAndMessage.Add(contact.Key.Email, contact.Value);
            }

            messageSenderBusiness.SendMessage(generalBirthdayReminderContactAndMessage);
        }

        #region Private Methods
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
                .AddSingleton<IMessageParserBusiness, MessageParserBusiness>()
                .AddSingleton<IMessageSenderBusiness, MailSenderBusiness>()
                .AddSingleton<IMessageSenderBusiness, TextSenderBusiness>()
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
        #endregion
    }
}
