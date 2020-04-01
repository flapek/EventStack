using EventStack_API.Helpers;
using EventStack_API.Models;
using Interfaces;
using NUnit.Framework;

namespace EventStack_API.UnitTest
{
    public class DbContextTest
    {
        private DbFactory<IOrganization> dbFactory;
        [SetUp]
        public void Setup()
        {
            dbFactory = new DbContext();
        }

        [Test]
        public void insertOne()
        {
            Assert.Pass();
        }
    }
}