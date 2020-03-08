using System;

namespace BirthdayGreetings.Interfaces
{
    public interface IDateBusiness
    {
        private const int _leapYearMarchDay = 29;
        private const int _marchMontNumber = 3;

        DateTime GetDate() => DateTime.Now;

        int GetMarchMonthNumber() => _marchMontNumber;

        int GetLeapYearMarchDay() => _leapYearMarchDay;

        bool IsLeapYear(DateTime date) => IsLeapYear(date);
    }
}
