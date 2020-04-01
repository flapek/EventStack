using EventStack_API.Helpers;
using EventStack_API.Models;
using Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace EventStack_API.UnitTest
{
    public class DbContextTest
    {
        private DbFactory<IOrganization> dbFactory;
        private Mock<IOptions<DbSettings>> mockOption;
        private Mock<IMongoDatabase> mockDb;
        private Mock<IMongoClient> mockClient;

        [SetUp]
        public void Setup()
        {
            mockOption = new Mock<IOptions<DbSettings>>();
            mockDb = new Mock<IMongoDatabase>();
            mockClient = new Mock<IMongoClient>();
        }

        [Test]
        public void DbContext_CreateConstructor_Success()
        {
            var settings = new DbSettings()
            {
                Connection = "mongodb://tes123",
                DatabaseName = "TestDB"
            };

            mockOption.Setup(s => s.Value).Returns(settings);
            mockClient.Setup(c => c.GetDatabase(mockOption.Object.Value.DatabaseName, null))
                .Returns(mockDb.Object);

            dbFactory = new DbContext(mockOption.Object);

            Assert.NotNull(dbFactory);

        }
    }
}