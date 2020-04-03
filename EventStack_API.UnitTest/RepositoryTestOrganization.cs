using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DCouple.Mongo;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

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
        public void Insert_WhenNameOrPasswordOrEmailIsNullForOrganization_ThenReturnNull(
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

            var result = repositoryFactory.Insert(new Organization() { Name = name, Password = password, Email = email });
            result.Should().BeNull();
        }

        [Test]
        public void Insert_WhenModelIsValidAddToCollection_ThenCollectionCountIsOne()
        {
            var organizations = new List<Organization>();
            var settings = new DbSettings()
            {
                _connectionString = "mongodb://tes123",
                _databaseName = "TestDB"
            };
            var mockOption = new Mock<IOptions<DbSettings>>();
            mockOption.Setup(s => s.Value).Returns(settings);
            var databaseMock = new Mock<IMongoDatabase>();
            var peopleCollectionMock = new Mock<IMongoCollection<Organization>>();
            var validator = Mock.Of<IDbModelValidator>(validator => validator.Validate(It.IsAny<IDbModel>()) == true);
            var dbContextMock = new Mock<DbContext>(mockOption.Object);
            var repositoryFactory = new Repository<Organization>(dbContextMock.Object, validator);

            //dbContextMock.Setup(x => x.GetCollection<Organization>(typeof(Organization).Name)).Returns(() => new FakeCollection<Organization>());



            organizations.Should().HaveCount(1);
        }

        #endregion
    }
}
