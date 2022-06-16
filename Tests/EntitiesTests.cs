using System;
using Xunit;
using Entities;

namespace Tests
{
    public class EntitiesTests
    {
        [Fact]
        public void CorrectInitialization_ContactInformation()
        {
            // Arrange:
            int id = 1;
            string mail = "";
            string phoneNumber = "";

            // Act:
            ContactInformation contactInformation = new(id, mail, phoneNumber);

            // Assert:
            Assert.True(contactInformation.Id > 0);
            Assert.Contains("@", contactInformation.Mail);
        }
    }
}
