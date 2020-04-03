using EventStack_API.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using FluentAssertions;
using Models;
using EventStack_API.Interfaces;
using MongoDB.Driver;

namespace EventStack_API.UnitTest
{
    class RepositoryTestOrganization
    {
        [SetUp]
        public void Setup()
        {
        }

        #region insert Test

        [Test]
        [Combinatorial]
        public void insert_WhenNameOrPasswordOrEmailIsNullForOrganization_ThenReturnNull(
            [Values(null, "Jan")] string name,
            [Values(null, "@j3st1234")] string password,
            [Values(null, "jan.test@test.com")] string email)
        {
            var settings = new DbSettings()
            {
                _connectionString = "mongodb://tes123",
                _databaseName = "TestDB"
            };
            var mockOption = new Mock<IOptions<DbSettings>>();
            mockOption.Setup(s => s.Value).Returns(settings);
            var validator = Mock.Of<IDbModelValidator>(validator => validator.Validate(It.IsAny<IDbModel>()) == false);
            var dbContextMock = new Mock<DbContext>(mockOption.Object);
            var repositoryFactory = new Repository<Organization>(dbContextMock.Object, validator);

            if (name != null && password != null && email != null)
                return;

            var result = repositoryFactory.insert(new Organization() { Name = name, Password = password, Email = email });
            result.Should().BeNull();
        }

        [TestCase("Jan", "@j3st1234", "jan.test@test.com")]
        public void insert_WhenNameOrPasswordOrEmailIsNotNullForOrganization_ThenReturnOrganizaction(string name, string password, string email)
        {
            var settings = new DbSettings()
            {
                _connectionString = "mongodb://tes123",
                _databaseName = "TestDB"
            };
            var mockOption = new Mock<IOptions<DbSettings>>();
            mockOption.Setup(s => s.Value).Returns(settings);
            var validator = Mock.Of<IDbModelValidator>(validator => validator.Validate(It.IsAny<IDbModel>()) == true);
            var dbContextMock = new Mock<DbContext>(mockOption.Object);
            var mockCollection = new Mock<IMongoCollection<Organization>>();
            dbContextMock.Setup(x => x.GetCollection<Organization>(typeof(Organization).Name)).Returns(mockCollection.Object);
            IRepositoryFactory<Organization> repositoryFactory = new Repository<Organization>(dbContextMock.Object, validator);
            var expected = new Organization() { Name = name, Password = password, Email = email };

            repositoryFactory.insert(expected).Should().BeSameAs(expected);
        }

        #endregion
    }
}
