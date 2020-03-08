using BirthdayGreetings.DataAccess;
using BirthdayGreetings.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BirthdayGreetings.Business
{
    public class PeopleBusiness : IPeopleBusiness
    {
        private readonly BirthdayGreetingContext _context;
        private readonly IDateBusiness _dateBusiness;

        public PeopleBusiness(BirthdayGreetingContext context, IDateBusiness dateBusiness)
        {
            _context = context;
            _dateBusiness = dateBusiness;
        }

        /// <summary>
        /// Get all people
        /// </summary>
        /// <returns></returns>
        public List<Person> GetAllPeople()
        {
            return _context.Person.ToList();
        }

        /// <summary>
        /// Get all people who's birthday is today
        /// </summary>
        /// <returns></returns>
        public List<Person> GetBirthDayPeople()
        {
            var today = _dateBusiness.GetDate();
            int day = today.Day;
            int year = today.Year;
            int month = today.Month;

            var people = _context.Person
                .Where(
                p => (month == _dateBusiness.GetMarchMonthNumber() && _dateBusiness.IsLeapYear(today) ?
                    (p.DateOfBirth.Day == day || p.DateOfBirth.Day == _dateBusiness.GetLeapYearMarchDay()) : p.DateOfBirth.Day == day)
                    && p.DateOfBirth.Month == month)
                .ToList();

            return people;
        }
    }
}