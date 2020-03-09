using BirthdayGreetings.DataAccess;
using System.Collections.Generic;

namespace BirthdayGreetings.Interfaces
{
    public interface IPeopleBusiness
    {
        List<Person> GetAllPeople();

        List<Person> GetBirthDayPeople();
    }
}