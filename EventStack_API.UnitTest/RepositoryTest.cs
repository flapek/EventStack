using EventStack_API.Helpers;
using EventStack_API.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using FluentAssertions;
using Models;

namespace EventStack_API.UnitTest
{
    public class RepositoryTest
    {
        private DbFactory<Organization> dbFactory;
        private Mock<DbContext> dbContextMock;
        private Mock<IOptions<DbSettings>> mockOption;

        [SetUp]
        public void Setup()
        {
            var settings = new DbSettings()
            {
                _connectionString = "mongodb://tes123",
                _databaseName = "TestDB"
            };

            mockOption = new Mock<IOptions<DbSettings>>();
            mockOption.Setup(s => s.Value).Returns(settings);
            dbContextMock = new Mock<DbContext>(mockOption.Object);

            dbFactory = new OrganizationRepository(dbContextMock.Object);

            //dbFactory = new OrganizationRepository(Mock.Of<DbContext>(option => option.GetCollection<Organization>("Organization") == ))
        }

        #region insert Test

        [Test]
        public void insert_WhenInputIsNull_ThenArgumentNullExceptionIsThrown()
        {
            Organization organization = null;
            Action action = () => dbFactory.insert(organization);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestCase("Jan", "@j3st1234", "jan.test@test.com")]
        public void insert_WhenInputIdIsNotSet_ThenGenerateId(string name, string password, string email) =>
            dbFactory.insert(new Organization() { Name = name, Password = password, Email = email }).Id.Should().NotBeNull();

        [Test]
        [Combinatorial]
        public void insert_WhenNameOrPasswordOrEmailIsNull_ThenReturnNull(
            [Values(null, "Jan")] string name,
            [Values(null, "@j3st1234")] string password,
            [Values(null, "jan.test@test.com")] string email)
        {
            if (name != null && password != null && email != null)
                return;

            var result = dbFactory.insert(new Organization() { Name = name, Password = password, Email = email });
            result.Should().BeNull();
        }

        [TestCase("Jan", "@j3st1234", "jan.test@test.com")]
        public void insert_WhenNameOrPasswordOrEmailIsNotNull_ThenReturnOrganizaction(string name, string password, string email)
        {
            var expected = new Organization() { Name = name, Password = password, Email = email };
            dbFactory.insert(expected).Should().BeSameAs(expected);
        }

        #endregion
    }
}
