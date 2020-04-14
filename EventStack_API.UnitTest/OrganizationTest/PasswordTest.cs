using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class PasswordTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();

        [TestCase("EventStack123!")]
        [TestCase("#JamesBond007")]
        [TestCase("$GrzesiuSzlac91$")]
        [TestCase("8^2=6Cztery")]
        [TestCase("Koronawirus,2020")]
        [TestCase("JestemLegenda(2007)")]
        public void Organization_IsRegexAcceptPassword_True(string password)
        {
            organization.Password = password;
            Assert.IsTrue((organization as object).isValid("Password", "Password must contain"));
        }

        [TestCase("123456")]
        [TestCase("Admin123")]
        [TestCase("Haslo")]
        [TestCase("00000")]
        [TestCase("asdfghijklm")]
        [TestCase("sexbomb69")]
        public void Organization_IsRegexRejectPassword_False(string password)
        {
            organization.Password = password;
            Assert.IsFalse((organization as object).isValid("Password", "Password must contain"));
        }

        [Test]
        public void Organization_IsPasswordRequired_False()
        {
            organization.Password = null;
            Assert.IsFalse((organization as object).isValid("Password", "Password must be set!"));
        }

        [Test]
        public void Organization_IsPasswordCanBeNotNull_True()
        {
            organization.Password = "not null";
            Assert.IsTrue((organization as object).isValid("Password", "Password must be set!"));            
        }

        [Test]
        public void Organization_IsPasswordHasMaximumOfCharacters_False()
        {
            organization.Password = new string('*', 31);
            Assert.IsFalse((organization as object).isValid("Password", "The maximum number"));            
        }
    }
}