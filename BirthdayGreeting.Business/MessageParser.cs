using BirthdayGreetings.DataAccess;
using BirthdayGreetings.Interfaces;
using System;
using System.Collections.Generic;

namespace BirthdayGreeting.Business
{
    public class MessageParser : IMessageParser
    {
        public string CreateIndividualBirthDayReminder(Dictionary<string, string> birthDayPersonInfo, List<Person> peopleToNotify)
        {
            throw new NotImplementedException();
        }

        public string CreatePersonalBirthDayWish(Dictionary<string, string> birthDaypeopleInfo)
        {
            throw new NotImplementedException();
        }

        public string CreateSingleBirthDayReminder(string personName, Dictionary<string, string> birthDayPeopleInfo)
        {
            throw new NotImplementedException();
        }
    }
}
