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
        private DbContext dbContext;
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

            dbContext = new DbContext(mockOption.Object);
        }

        #region constructor Test

        [Test]
        public void dbContext_CreateConstructor_Success() => dbContext.Should().NotBeNull();

        #endregion

    }
}