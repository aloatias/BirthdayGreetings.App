﻿using BirthdayGreetings.DataAccess;
using System;
using System.Collections.Generic;

namespace BirthdayGreetings.Interfaces
{
    public interface IMessageParserBusiness
    {
        Dictionary<Person, string> CreateGeneralBirthdayReminder(List<Person> receiverPeople, List<Person> peopleOnBirthday);

        List<Tuple<Person, string>> CreateIndividualBirthdayReminder(List<Person> peopleOnBirthday, List<Person> peopleToNotify);

        Dictionary<Person, string> CreatePersonalBirthdayWish(List<Person> peopleOnBirthday);
    }
}