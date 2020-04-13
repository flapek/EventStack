using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class PasswordTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp()
        {
            organization = new Organization();
        }

        [TestCase("EventStack123!")]
        [TestCase("#JamesBond007")]
        [TestCase("$GrzesiuSzlac91$")]
        [TestCase("8^2=6Cztery")]
        [TestCase("Koronawirus,2020")]
        [TestCase("JestemLegenda(2007)")]
        public void Organization_IsRegexAcceptPassword_True(string password)
        {
            organization.Password = password;
            Assert.IsTrue(!ValidateModel(organization).Any(a => a.MemberNames.Contains("Password") && a.ErrorMessage.Contains("Password must contain")));
        }

        [TestCase("123456")]
        [TestCase("Admin123")]
        [TestCase("Haslo")]
        [TestCase("00000")]
        [TestCase("asdfghijklm")]
        [TestCase("sexbomb69")]
        public void Organization_IsRegexRejectPassword_True(string password)
        {
            organization.Password = password;
            Assert.IsTrue(ValidateModel(organization).Any(a => a.MemberNames.Contains("Password") && a.ErrorMessage.Contains("Password must contain")));
        }

        [Test]
        public void Organization_IsPasswordRequired_True()
        {
            organization.Password = null;
            Assert.IsTrue(ValidateModel(organization).Any(a => a.MemberNames.Contains("Password") && a.ErrorMessage.Contains("Password must be set!")));
        }

        [Test]
        public void Organization_IsPasswordNotRequired_False()
        {
            organization.Password = "not null";
            Assert.IsFalse(ValidateModel(organization).Any(a => a.MemberNames.Contains("Password") && a.ErrorMessage.Contains("Password must be set!")));
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}