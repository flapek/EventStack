using EventStack_API.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using FluentAssertions;
using Models;
using EventStack_API.Interfaces;

namespace EventStack_API.UnitTest
{
    [TestFixture(typeof(Organization))]
    [TestFixture(typeof(Category))]
    [TestFixture(typeof(Event))]
    public class GenericRepositoryTest<T> where T : IDbModel
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
            var dbContextMock = new Mock<MongoDbContext>(mockOption.Object);
            IRepositoryFactory<T> repositoryFactory = new MongoRepository<T>(dbContextMock.Object, validator.Object);

            Action action = () => repositoryFactory.Insert(It.IsAny<T>());

            action.Should().Throw<ArgumentNullException>();
        }

        #endregion
    }
}
