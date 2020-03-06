using System;

namespace BirthdayGreetings.Interfaces
{
    public interface IDateBusiness
    {
        private const int _leapYearMarchDay = 29;

        int GetLeapYearMarchDay() => _leapYearMarchDay;

        DateTime GetDate() => DateTime.Now;

        bool IsLeapYear(DateTime date) => IsLeapYear(date);
    }
}
