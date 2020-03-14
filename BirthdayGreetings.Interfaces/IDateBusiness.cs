﻿using System;

namespace BirthdayGreetings.Interfaces
{
    public interface IDateBusiness
    {
        private const int _leapYearMarchDay = 29;
        private const int _marchMonthNumber = 3;

        DateTime GetDate() => DateTime.Now;

        int GetMarchMonthNumber() => _marchMonthNumber;

        int GetLeapYearMarchDay() => _leapYearMarchDay;

        bool IsLeapYear(DateTime date) => DateTime.IsLeapYear(date.Year);
    }
}
