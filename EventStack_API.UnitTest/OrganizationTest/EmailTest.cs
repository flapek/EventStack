using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class EmailTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();

        [TestCase("eventstack@gmail.com")]
        public void Organization_IsRegexAcceptEmail_True(string email)
        {
            organization.Email = email;
            Assert.IsTrue((organization as object).isValid("Email", "Email must contain"));
        }

        [TestCase("@com")]
        public void Organization_IsRegexRejectEmail_False(string email)
        {
            organization.Email = email;
            Assert.IsFalse((organization as object).isValid("Email", "Email must contain"));
        }

        [Test]
        public void Organization_IsEmailRequired_False()
        {
            organization.Email = null;
            Assert.IsFalse((organization as object).isValid("Email", "Email must be set!"));
        }

        [Test]
        public void Organization_IsEmailCanBeNotNull_True()
        {
            organization.Email = "not null";
            Assert.IsTrue((organization as object).isValid("Email", "Email must be set!"));
        }

        [Test]
        public void Organization_IsEmailHasMaximumOfCharacters_False()
        {
            organization.Email = new string('*', 101);
            Assert.IsFalse((organization as object).isValid("Email", "The maximum number"));
        }
    }
}