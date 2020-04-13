using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class EmailTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp()
        {
            organization = new Organization();
        }

        [TestCase("eventstack@gmail.com")]
        public void Organization_IsRegexAcceptEmail_True(string email)
        {
            organization.Email = email;
            Assert.IsTrue(!ValidateModel(organization).Any(a => a.MemberNames.Contains("Email") && a.ErrorMessage.Contains("Email must contain")));
        }

        [TestCase("@com")]
        public void Organization_IsRegexRejectEmail_True(string email)
        {
            organization.Email = email;
            Assert.IsTrue(ValidateModel(organization).Any(a => a.MemberNames.Contains("Email") && a.ErrorMessage.Contains("Email must contain")));
        }

        public void Organization_IsEmailRequired_True()
        {
            organization.Email = null;
            Assert.IsTrue(ValidateModel(organization).Any(a => a.MemberNames.Contains("Email") && a.ErrorMessage.Contains("Email must be set!")));
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