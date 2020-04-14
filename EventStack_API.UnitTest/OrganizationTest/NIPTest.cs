using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class NIPTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp()
        {
            organization = new Organization();
        }

        [TestCase("1234567890")]
        public void Organization_IsRegexAcceptNIP_False(string NIP)
        {
            organization.NIP = NIP;
            Assert.IsFalse(ValidateModel(organization).Any(a => a.MemberNames.Contains("NIP") && a.ErrorMessage.Contains("NIP must contain")));
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