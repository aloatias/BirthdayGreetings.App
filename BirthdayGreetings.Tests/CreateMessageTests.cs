using BirthdayGreetings.Business;
using BirthdayGreetings.Interfaces;
using BirthdayGreetings.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BirthdayGreetings.Message.Tests
{
    public class CreateMessageTests : TestFactory
    {
        private readonly IMessageParserBusiness _messageParser;

        public CreateMessageTests()
        {
            _messageParser = new MessageParserBusiness();
        }

        [Fact(DisplayName = "Should create birthday messages for ten different people")]
        public void Should_CreatePersonalBirthdaysMessagesForTenPeople()
        {
            // Prepare
            List<DataAccess.Person> peopleOnBirthday = CreateFakePeople(10);
            var expectedResult = new Dictionary<DataAccess.Person, string>();

            peopleOnBirthday.ForEach(p =>
            {
                var message = new StringBuilder();

                message.AppendLine("Subject: Happy birthday!");
                message.AppendLine();
                message.AppendLine($"Happy birthday, dear { p.FirstName }!");

                expectedResult.Add(p, message.ToString());
            });

            // Act
            var actualResult = _messageParser.CreatePersonalBirthdayWish(peopleOnBirthday);

            // Test
            foreach (var er in expectedResult)
            {
                Assert.True(actualResult.ContainsKey(er.Key));
                Assert.True(actualResult.ContainsValue(er.Value));
            }
        }

        [Fact(DisplayName = "Should create individual birthday reminder messages for twenty people (ten messages per person)")]
        public void Should_CreateIndividualBirthdayRemindersForTwentyPeople()
        {
            // Prepare
            List<DataAccess.Person> peopleOnBirthday = CreateFakePeople(10);
            List<DataAccess.Person> peopleToNotify = CreateFakePeople(20);

            var expectedResult = new List<Tuple<DataAccess.Person, string>>();

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

                        expectedResult.Add(Tuple.Create(pTn, sb.ToString()));
                    }
                });
            });

            // Act
            var actualResult = _messageParser.CreateIndividualBirthdayReminder(peopleOnBirthday, peopleToNotify);

            // Test
            actualResult.ForEach(ar => {
                expectedResult.Contains(ar);
            });
        }

        [Fact(DisplayName = "Should create a general birthdays reminder message for 10 people")]
        public void Should_CreateGeneralBirthdayReminder()
        {
            // Prepare
            List<DataAccess.Person> peopleOnBirthday = CreateFakePeople(5);
            List<DataAccess.Person> peopleToNotify = CreateFakePeople(10);

            var expectedResult = new Dictionary<DataAccess.Person, string>();

            peopleToNotify.ForEach(rp => {
                var message = new StringBuilder();

                message.AppendLine("Subject: Birthday Reminder!");
                message.AppendLine($"Dear: { rp.FirstName },");
                message.AppendLine();
                message.Append("Today is ");

                bool atLeastOneMessage = false;
                for (var i = 0; i < peopleOnBirthday.Count; i++)
                {
                    if (peopleOnBirthday[i] != peopleToNotify[i])
                    {
                        atLeastOneMessage = true;

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
                }

                if (atLeastOneMessage)
                {
                    message.AppendLine("'s birthday.");
                    message.AppendLine("Don't forget to send them a message!");
                    expectedResult.Add(rp, message.ToString());
                }
            });

            // Act
            var actualResult = _messageParser.CreateGeneralBirthdayReminder(peopleOnBirthday, peopleToNotify);

            // Test
            foreach (var er in expectedResult)
            {
                Assert.True(actualResult.ContainsKey(er.Key));
                Assert.True(actualResult.ContainsValue(er.Value));
            }
        }
    }
}
