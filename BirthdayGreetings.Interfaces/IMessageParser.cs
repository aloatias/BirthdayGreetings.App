using BirthdayGreetings.DataAccess;
using System;
using System.Collections.Generic;

namespace BirthdayGreetings.Interfaces
{
    public interface IMessageParserBusiness
    {
        Dictionary<Person, string> CreateGeneralBirthDayReminder(List<Person> receiverPeople, List<Person> peopleOnBirthday);

        List<Tuple<Person, string>> CreateIndividualBirthDayReminder(List<Person> peopleOnBirthday, List<Person> peopleToNotify);

        Dictionary<Person, string> CreatePersonalBirthDayWish(List<Person> peopleOnBirthday);
    }
}