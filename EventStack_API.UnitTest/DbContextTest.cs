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

        [SetUp]
        public void Setup()
        {
            mockOption = new Mock<IOptions<DbSettings>>();

            var settings = new DbSettings()
            {
                Connection = "mongodb://tes123",
                DatabaseName = "TestDB"
            };

            mockOption.Setup(s => s.Value).Returns(settings);
            dbFactory = new DbContext(mockOption.Object);
        }


        #region insertOne Test

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

        //incorect test
        //TODO
        [TestCase("Jan", "@j3st1234", "jan.test@test.com")]
        public void insertOne_WhenModelIsValidAddItToDb_ThenDbCollectionHasOneElement(string name, string password, string email)
        {
            dbFactory.insertOne(new Organization() { Name = name, Password = password, Email = email });
            var db = (DbContext)dbFactory;
            var collection = db.MongoDatabase.GetCollection<Organization>("Organizaction");
            collection.Should().NotBeNull();
        }

        #endregion
    }
}