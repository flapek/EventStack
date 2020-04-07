using EventStack_API.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace EventStack_API.UnitTest
{
    public class DbContextTest
    {
        private MongoDbContext dbContext;
        private Mock<IOptions<DbSettings>> mockOption;

        [SetUp]
        public void Setup()
        {
            var settings = new DbSettings()
            {
                ConnectionString = "mongodb://tes123",
                DatabaseName = "TestDB"
            };

            mockOption = new Mock<IOptions<DbSettings>>();
            mockOption.Setup(s => s.Value).Returns(settings);

            dbContext = new MongoDbContext(mockOption.Object);
        }

        #region constructor Test

        [Test]
        public void dbContext_CreateConstructor_Success() => dbContext.Should().NotBeNull();

        #endregion

    }
}