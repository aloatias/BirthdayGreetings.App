using BirthdayGreeting.Business;
using BirthdayGreetings.Interfaces;
using BirthdayGreetings.Tests;
using Microsoft.Extensions.Logging;
using System;
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

        [Fact(DisplayName = "Should send 50 invidual birthday reminder emails")]
        public void Should_SendFiftyBirthdayRemindersEmails()
        {
            // Prepare
            List<DataAccess.Person> peopleOnBirthday = CreateFakePeople(5);
            List<DataAccess.Person> peopleToNotify = CreateFakePeople(10);

            var contactAndMessage = new List<Tuple<string, string>>();

            peopleToNotify.ForEach(pTn =>
            {
                peopleOnBirthday.ForEach(pOb =>
                {
                    if (pTn != pOb)
                    {
                        var sb = new StringBuilder();

                        sb.AppendLine("Subject: Birthday Reminder!");
                        sb.AppendLine();
                        sb.AppendLine($"Dear {pTn.FirstName }");
                        sb.AppendLine();
                        sb.AppendLine($"Today is { pOb.FirstName } { pOb.LastName }' birthday.");
                        sb.AppendLine("Don't forget to send them a message!");

                        contactAndMessage.Add(Tuple.Create(pTn.Email, sb.ToString()));
                    }
                });
            });

            // Act
            _messageSenderBusiness.SendMessage(contactAndMessage);
        }

        [Fact(DisplayName = "Should send one general birthday reminder message")]
        public void Should_SendOneGeneralBirthdayReminder()
        {
            // Prepare
            List<DataAccess.Person> peopleOnBirthday = CreateFakePeople(5);
            List<DataAccess.Person> peopleToNotify = CreateFakePeople(10);

            var emailAddressAndMessage = new Dictionary<string, string>();

            peopleToNotify.ForEach(rp => {
                var message = new StringBuilder();

                message.AppendLine("Subject: Birthday Reminder!");
                message.AppendLine($"Dear: { rp.FirstName },");
                message.AppendLine();
                message.Append("Today is ");

                for (var i = 0; i < peopleOnBirthday.Count; i++)
                {
                    message.Append($"{ peopleOnBirthday[i].FirstName } { peopleOnBirthday[i].LastName }");

                    if (i + 2 != peopleOnBirthday.Count)
                    {
                        message.Append(", ");
                    }
                    else
                    {
                        message.Append($" and { peopleOnBirthday[i++].FirstName } { peopleOnBirthday[i++].LastName }");
                        i += 2;
                    }
                }

                message.AppendLine("'s birthday.");
                message.AppendLine("Don't forget to send them a message!");
                emailAddressAndMessage.Add(rp.Email, message.ToString());
            });

            // Act
            _messageSenderBusiness.SendMessage(emailAddressAndMessage);
        }
    }
}
