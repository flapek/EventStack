using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class DestriptionTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp()
        {
            organization = new Organization();
        }

        [Test]
        public void Organization_IsDestriptionHasMaximumOfCharacters_True()
        {
            organization.Destription = new string('*', 1001);
            Assert.IsTrue(ValidateModel(organization).Any(a => a.MemberNames.Contains("Destription") && a.ErrorMessage.Contains("The maximum number")));
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