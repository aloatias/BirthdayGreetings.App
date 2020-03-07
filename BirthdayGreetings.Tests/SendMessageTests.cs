using BirthdayGreeting.Business;
using BirthdayGreetings.Interfaces;
using BirthdayGreetings.Tests;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BirthdayGreetings.Message.Tests
{
    public class SendMessageTests : TestFactory
    {
        private readonly IMessageSenderBusiness _messageSenderBusiness;

        public SendMessageTests()
        {
            var logger = new LoggerFactory().CreateLogger<MailSenderBusiness>();
            _messageSenderBusiness = new MailSenderBusiness(logger);
        }

        [Fact(DisplayName = "Should send 10 email to people who's birthday is today")]
        public void Should_SendEmailsToBirthdayPeople()
        {
            // Prepare
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

            // Act
            _messageSenderBusiness.SendMessage(contactsAndMessage);
        }
    }
}
