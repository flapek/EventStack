using EventStack_API.Models;
using NUnit.Framework;
using FluentAssertions;

namespace EventStack_API.UnitTest
{
    public class DbContextTest
    {
        private MongoDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var settings = new DbSettings()
            {
                ConnectionString = "mongodb://tes123",
                DatabaseName = "TestDB"
            };

            dbContext = new MongoDbContext(settings);
        }

        #region constructor Test

        [Test]
        public void dbContext_CreateConstructor_Success() => dbContext.Should().NotBeNull();

        #endregion

    }
}