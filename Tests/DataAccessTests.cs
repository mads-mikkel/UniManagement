using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DataAccess;

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
    }
}
