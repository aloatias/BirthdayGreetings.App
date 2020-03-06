using BirthdayGreetings.DataAccess;
using BirthdayGreetings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BirthdayGreetings.Business
{
    public class BirthdayGreetingBusiness : IBirthdayGreetingBusiness
    {
        private readonly BirthdayGreetingContext _context;

        public BirthdayGreetingBusiness(BirthdayGreetingContext context)
        {
            _context = context;
        }

        public void SendIndividualBirthdayWish()
        {
            var today = DateTime.Now;
            var day = DateTime.Now.Day.ToString();
            var month = DateTime.Now.Month.ToString();

            if (today.Day == 28
                && today.Month == 03)
            {
                day = "29";
                month = "03";
            }

            List<Person> people = _context.Person
                .Where(p => p
                    .DateOfBirth.Day.ToString() == day
                    && p.DateOfBirth.Month.ToString() == month)
                .ToList();

            people.ForEach(p => {
                // Send Email
            });
        }
    }
}