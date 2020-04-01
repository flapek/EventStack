using EventStack_API.Helpers;
using EventStack_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using FluentAssertions;
using Models;

namespace EventStack_API.UnitTest
{
    public class DbContextTest
    {
        private DbFactory<Organization> dbFactory;
        private Mock<IOptions<DbSettings>> mockOption;
        private Mock<IMongoDatabase> mockDb;
        private Mock<IMongoClient> mockClient;

        [SetUp]
        public void Setup()
        {
            mockOption = new Mock<IOptions<DbSettings>>();
            mockDb = new Mock<IMongoDatabase>();
            mockClient = new Mock<IMongoClient>();

            var settings = new DbSettings()
            {
                Connection = "mongodb://tes123",
                DatabaseName = "TestDB"
            };

            mockOption.Setup(s => s.Value).Returns(settings);
            mockClient.Setup(c => c.GetDatabase(mockOption.Object.Value.DatabaseName, null))
                .Returns(mockDb.Object);

            dbFactory = new DbContext(mockOption.Object);
        }

        [Test]
        public void dbContext_CreateConstructor_Success() => dbFactory.Should().NotBeNull();

        [Test]
        public void insertOne_WhenIOrganizationIsNull_ThenArgumentNullExceptionIsThrown()
        {
            Action action = () => dbFactory.insertOne(null);
            action.Should().Throw<ArgumentNullException>();
        }
    }
}