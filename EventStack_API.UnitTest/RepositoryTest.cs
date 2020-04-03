using EventStack_API.Helpers;
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
    [TestFixture(typeof(Organization))]
    [TestFixture(typeof(Category))]
    [TestFixture(typeof(Event))]
    public class RepositoryTest<T> where T : IBaseDbModel
    {
        [SetUp]
        public void Setup()
        {
        }

        #region insert Test

        [Test]
        public void insert_WhenInputIsNull_ThenArgumentNullExceptionIsThrown()
        {
            var settings = new DbSettings()
            {
                _connectionString = "mongodb://tes123",
                _databaseName = "TestDB"
            };
            var mockOption = new Mock<IOptions<DbSettings>>();
            mockOption.Setup(s => s.Value).Returns(settings);
            var validator = new Mock<IDbModelValidator>();
            var dbContextMock = new Mock<DbContext>(mockOption.Object);
            IRepository<T> dbFactory = new Repository<T>(dbContextMock.Object, validator.Object);

            Action action = () => dbFactory.insert((dynamic)null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestCase("Jan", "@j3st1234", "jan.test@test.com")]
        public void insert_WhenInputIdIsNotSet_ThenGenerateId(string name, string password, string email)
        {
            var settings = new DbSettings()
            {
                _connectionString = "mongodb://tes123",
                _databaseName = "TestDB"
            };
            var mockOption = new Mock<IOptions<DbSettings>>();
            mockOption.Setup(s => s.Value).Returns(settings);
            var validator = Mock.Of<IDbModelValidator>(validator => validator.Validate(It.IsAny<IBaseDbModel>()) == false);
            var dbContextMock = new Mock<DbContext>(mockOption.Object);

            IRepository<Organization> dbFactory = new Repository<Organization>(dbContextMock.Object, validator);
            validator = Mock.Of<IDbModelValidator>(validator => validator.Validate(It.IsAny<IBaseDbModel>()) == true);
            var collection = Mock.Of<IMongoCollection<IBaseDbModel>>();
            var context = Mock.Of<IDbContext>(context => context.GetCollection<Organization>(typeof(Organization).Name) == collection);
            dbFactory = new Repository<Organization>(dbContextMock.Object, validator);

            dbFactory.insert(new Organization() { Name = name, Password = password, Email = email }).Id.Should().NotBeNull();
        }

        [Test]
        [Combinatorial]
        public void insert_WhenNameOrPasswordOrEmailIsNull_ThenReturnNull(
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
            var validator = Mock.Of<IDbModelValidator>(validator => validator.Validate(It.IsAny<IBaseDbModel>()) == false);
            var dbContextMock = new Mock<DbContext>(mockOption.Object);
            var dbFactory = new Repository<Organization>(dbContextMock.Object, validator);

            if (name != null && password != null && email != null)
                return;

            var result = dbFactory.insert(new Organization() { Name = name, Password = password, Email = email });
            result.Should().BeNull();
        }

        [TestCase("Jan", "@j3st1234", "jan.test@test.com")]
        public void insert_WhenNameOrPasswordOrEmailIsNotNull_ThenReturnOrganizaction(string name, string password, string email)
        {
            var settings = new DbSettings()
            {
                _connectionString = "mongodb://tes123",
                _databaseName = "TestDB"
            };
            var mockOption = new Mock<IOptions<DbSettings>>();
            mockOption.Setup(s => s.Value).Returns(settings);
            var validator = Mock.Of<IDbModelValidator>(validator => validator.Validate(It.IsAny<IBaseDbModel>()) == false);
            var dbContextMock = new Mock<DbContext>(mockOption.Object);

            IRepository<Organization> dbFactory = new Repository<Organization>(dbContextMock.Object, validator);
            var expected = new Organization() { Name = name, Password = password, Email = email };
            dbFactory.insert(expected).Should().BeSameAs(expected);
        }

        #endregion
    }
}
