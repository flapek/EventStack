using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class NameTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();

        [Test]
        public void Organization_IsNameRequired_False()
        {
            organization.Name = null;
            Assert.IsFalse((organization as object).isValid("Name", "Name must be set!"));
        }

        [Test]
        public void Organization_IsNameCanBeNotNull_True()
        {
            organization.Name = "not null";
            Assert.IsTrue((organization as object).isValid("Name", "Name must be set!"));
        }

        [Test]
        public void Organization_IsNameHasMaximumOfCharacters_False()
        {
            organization.Name = new string('*', 101);
            Assert.IsFalse((organization as object).isValid("Name", "The maximum number"));
        }
    }
}