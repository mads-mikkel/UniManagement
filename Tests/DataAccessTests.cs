using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DataAccess;
using Entities;

namespace Tests
{
    public class DataAccessTests
    {
        [Fact]
        public void CanConnectToDb()
        {
            // Arrange:
            Repository repo;

            // Act:
            repo = new();

            //
            Assert.NotNull(repo);
        }

        [Fact]
        public void GetAllPersonsReturnsDataTest()
        {
            // Arrange:
            Repository repo = new();

            // Act:
            List<Person> people = repo.GetAllPersons();

            // Assert:
            Assert.True(people.Count > 0);
        }
    }
}
