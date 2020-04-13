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
        private string errorMessage = "Password must contain at least 1 lowercase and uppercase alphabetical character, 1 numeric character, 1 special character(!,@,#,$,%,^,&,*) and must be eight characters or longer!";

        [SetUp]
        public void SetUp()
        {
            organization = new Organization();
        }

        [TestCase("EventStack123!")]
        public void Organization_IsRegexAcceptPassword_True(string password)
        {
            organization.Password = password;

            Assert.IsTrue(!ValidateModel(organization).Any(a => a.MemberNames.Contains("Password") && a.ErrorMessage.Contains(errorMessage)));
        }

        [TestCase("123456")]
        public void Organization_IsRegexRejectPassword_True(string password)
        {
            organization.Password = password;

            Assert.IsTrue(ValidateModel(organization).Any(a => a.MemberNames.Contains("Password") && a.ErrorMessage.Contains(errorMessage)));
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