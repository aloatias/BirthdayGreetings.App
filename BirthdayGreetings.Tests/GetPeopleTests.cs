using BirthdayGreetings.Business;
using BirthdayGreetings.DataAccess;
using BirthdayGreetings.Interfaces;
using BirthdayGreetings.Tests;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BirthdayGreetings.Person.Tests
{
    public class GetPeopleTests : TestFactory
    {
        private readonly BirthdayGreetingContext _context;
        private readonly IPeopleBusiness _peopleBusiness;
        private readonly Mock<IDateBusiness> _dateBusiness;

        public GetPeopleTests()
        {
            _context = CreateContext();
            _dateBusiness = MockMe<IDateBusiness>();
            _peopleBusiness = new PeopleBusiness(_context, _dateBusiness.Object);
        }

        [Fact(DisplayName = "Should get ten people")]
        public void Should_GetTenPeople()
        {
            // Prepare
            List<DataAccess.Person> expectedResult = CreateFakePeople(10);
            SeedPeopleInMemory(expectedResult);

            // Act
            var actualResult = _peopleBusiness.GetAllPeople();

            // Test
            Assert.True(actualResult.Count == expectedResult.Count);
        }

        [Fact(DisplayName = "Should get at least one person who's birthdays are on 28/03 or 29/03")]
        public void Should_GetPeopleBirthdayOnLeapYearEndOfMarch()
        {
            // Prepare
            List<DataAccess.Person> expectedResult = CreateFakePeople(1000);

            // Add at least one person who's birthday is on 29/03
            DateTime.TryParse("29/03/1988", out var leapYearMarchDay);
            var myTestPerson = new DataAccess.Person { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", DateOfBirth = leapYearMarchDay };
            expectedResult.Add(myTestPerson);
            SeedPeopleInMemory(expectedResult);

            // Mock DateBusiness
            DateTime.TryParse("28/03/2020", out var parsedDate);
            _dateBusiness.Setup(d => d.GetDate()).Returns(parsedDate);
            _dateBusiness.Setup(d => d.IsLeapYear(parsedDate)).Returns(true);            
            _dateBusiness.Setup(d => d.GetLeapYearMarchDay()).Returns(29);

            // Act
            var actualResult = _peopleBusiness.GetBirthDayPeople();

            // Test
            Assert.Contains(actualResult, p => p == myTestPerson);
        }

        [Fact(DisplayName = "Should get at least one person who's birthday's today")]
        public void Should_GetPeopleBirthdayToday()
        {
            // Prepare
            List<DataAccess.Person> expectedResult = CreateFakePeople(1000);

            // Add at least one person who's birthday is today
            var myTestPerson = new DataAccess.Person { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", DateOfBirth = DateTime.Now.AddYears(-30) };
            expectedResult.Add(myTestPerson);
            SeedPeopleInMemory(expectedResult);

            // Mock DateBusiness
            _dateBusiness.Setup(d => d.GetDate()).Returns(DateTime.Now);
            _dateBusiness.Setup(d => d.IsLeapYear(DateTime.Now)).Returns(false);

            // Act
            var actualResult = _peopleBusiness.GetBirthDayPeople();

            // Test
            Assert.Contains(actualResult, p => p == myTestPerson);
        }

        #region Private Methods

        private void SeedPeopleInMemory(List<DataAccess.Person> people)
        {
            _context.Person.AddRange(people);
            _context.SaveChanges();
        }
        #endregion
    }
}
