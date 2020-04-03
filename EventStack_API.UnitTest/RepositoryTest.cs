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
    public class RepositoryTest<T> where T : IDbModel
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
            IRepositoryFactory<T> dbFactory = new Repository<T>(dbContextMock.Object, validator.Object);

            Action action = () => dbFactory.insert(It.IsAny<T>());

            action.Should().Throw<ArgumentNullException>();
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
            var validator = Mock.Of<IDbModelValidator>(validator => validator.Validate(It.IsAny<IDbModel>()) == false);
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
            var validator = Mock.Of<IDbModelValidator>(validator => validator.Validate(It.IsAny<IDbModel>()) == false);
            var dbContextMock = new Mock<DbContext>(mockOption.Object);

            IRepositoryFactory<Organization> dbFactory = new Repository<Organization>(dbContextMock.Object, validator);
            var expected = new Organization() { Name = name, Password = password, Email = email };
            dbFactory.insert(expected).Should().BeSameAs(expected);
        }

        #endregion
    }
}
