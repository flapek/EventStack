using EventStack_API.Helpers;
using EventStack_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using FluentAssertions;
using Models;
using System.Collections.Generic;
using MongoDB.Bson;

namespace EventStack_API.UnitTest
{
    public class DbContextTest
    {
        private DbFactory<Organization> dbFactory;
        private Mock<IOptions<DbSettings>> mockOption;
        private Mock<IMongoDatabase> mockDb;
        private Mock<MongoClient> mockClient;

        [SetUp]
        public void Setup()
        {
            mockOption = new Mock<IOptions<DbSettings>>();
            mockDb = new Mock<IMongoDatabase>();
            mockClient = new Mock<MongoClient>();

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

        #region constructor Test

        [Test]
        public void dbContext_CreateConstructor_Success() => dbFactory.Should().NotBeNull();

        #endregion

        #region insertOne Test

        [Test]
        public void insertOne_WhenInputIsNull_ThenArgumentNullExceptionIsThrown()
        {
            Action action = () => dbFactory.insertOne(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestCase("Jan", "@j3st1234", "jan.test@test.com")]
        public void insertOne_WhenInputIdIsNotSet_ThenGenerateId(string name, string password, string email) => 
            dbFactory.insertOne(new Organization() { Name = name, Password = password, Email = email }).Id.Should().NotBeNull();

        [Test]
        [Combinatorial]
        public void insertOne_WhenNameOrPasswordOrEmailIsNull_ThenReturnNull(
            [Values(null, "Jan")] string name,
            [Values(null, "@j3st1234")] string password,
            [Values(null, "jan.test@test.com")] string email)
        {
            if (name != null && password != null && email != null)
                return;

            var result = dbFactory.insertOne(new Organization() { Name = name, Password = password, Email = email });
            result.Should().BeNull();
        }

        [TestCase("Jan", "@j3st1234", "jan.test@test.com")]
        public void insertOne_WhenNameOrPasswordOrEmailIsNotNull_ThenReturnOrganizaction(string name, string password, string email)
        {
            var expected = new Organization() { Name = name, Password = password, Email = email };
            dbFactory.insertOne(expected).Should().BeSameAs(expected);
        }

        #endregion
    }
}