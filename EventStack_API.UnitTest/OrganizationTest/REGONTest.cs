using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class REGONTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp()
        {
            organization = new Organization();
        }

        [TestCase("123456789")]
        public void Organization_IsRegexAcceptREGON_False(string REGON)
        {
            organization.REGON = REGON;
            Assert.IsFalse(ValidateModel(organization).Any(a => a.MemberNames.Contains("REGON") && a.ErrorMessage.Contains("REGON must contain")));
        }

        [TestCase("a2345678b")]
        public void Organization_IsRegexRejectREGON_True(string REGON)
        {
            organization.REGON = REGON;
            Assert.IsTrue(ValidateModel(organization).Any(a => a.MemberNames.Contains("REGON") && a.ErrorMessage.Contains("REGON must contain")));
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