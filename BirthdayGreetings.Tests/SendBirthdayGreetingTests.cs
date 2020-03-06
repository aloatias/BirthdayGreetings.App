using BirthdayGreetings.Business;
using BirthdayGreetings.DataAccess;
using BirthdayGreetings.Interfaces;
using System;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class SendBirthdayGreetingTests : TestFactory
    {
        private readonly BirthdayGreetingContext _context;
        private readonly IBirthdayGreetingBusiness _birthdayGreetingBusiness;

        public SendBirthdayGreetingTests()
        {
            _context = CreateContext();
            _birthdayGreetingBusiness = new BirthdayGreetingBusiness(_context);
        }

        [Fact(DisplayName = "Should send and individual birthday wish to the birthday person")]
        public void Should_SendIndividualBirthdayGreeting()
        {
            // Prepare
            SeedPerson();

            // Act
            _birthdayGreetingBusiness.SendIndividualBirthdayWish();

            // Test
        }

        private void SeedPerson()
        {
            DateTime.TryParse("19880505", out var dateOfBirth);
            _context.Person.Add(new Person { FirstName = "John", LastName = "Doe", Email = "johndoe@gmail.com", DateOfBirth = dateOfBirth });
            _context.SaveChanges();
        }
    }
}
