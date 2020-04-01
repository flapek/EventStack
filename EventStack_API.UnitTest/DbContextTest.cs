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

namespace EventStack_API.UnitTest
{
    public class DbContextTest
    {
        private DbFactory<Organization> dbFactory;
        private Mock<IOptions<DbSettings>> mockOption;
        private Mock<IMongoDatabase> mockDb;
        private Mock<IMongoClient> mockClient;

        public static IEnumerable<TestCaseData> testCasesOrganizationsWithNullParameters
        {
            get
            {
                yield return new TestCaseData(new Organization() { Name = null });
                yield return new TestCaseData(new Organization() { Name = "Jan", Password = null });
                yield return new TestCaseData(new Organization() { Name = "Jan", Password = "@j3st", Email = null });
            }
        }

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
        public void insertOne_WhenInputIsNull_ThenArgumentNullExceptionIsThrown()
        {
            Action action = () => dbFactory.insertOne(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void insertOne_WhenInputIdIsNotSet_ThenGenerateId() => dbFactory.insertOne(new Organization() { Name = "Jan", Password = "@j3st", Email = "jan.test@test.com" }).Id.Should().NotBeNull();

        [TestCaseSource("testCasesOrganizationsWithNullParameters")]
        public void insertOne_WhenNameOrPasswordOrEmailIsNull_ThenReturnNull(Organization organization)
        {
            var result = dbFactory.insertOne(organization);
            result.Should().NotBeNull();
        }
    }
}