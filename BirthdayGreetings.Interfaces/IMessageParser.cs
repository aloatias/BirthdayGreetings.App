using BirthdayGreetings.DataAccess;
using System.Collections.Generic;

namespace BirthdayGreetings.Interfaces
{
    public interface IMessageParser
    {
        string CreatePersonalBirthDayWish(Dictionary<string, string> birthDaypeopleInfo);

        string CreateIndividualBirthDayReminder(Dictionary<string, string> birthDayPersonInfo, List<Person> peopleToNotify);

        string CreateSingleBirthDayReminder(string personName, Dictionary<string, string> birthDayPeopleInfo);
    }
}
