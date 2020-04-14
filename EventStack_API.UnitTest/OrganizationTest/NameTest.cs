using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class NameTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp()
        {
            organization = new Organization();
        }

        [Test]
        public void Organization_IsNameRequired_True()
        {
            organization.Name = null;
            Assert.IsTrue(ValidateModel(organization).Any(a => a.MemberNames.Contains("Name") && a.ErrorMessage.Contains("Name must be set!")));
        }

        [Test]
        public void Organization_IsNameCanBeNotNull_False()
        {
            organization.Id = "not null";
            Assert.IsFalse(ValidateModel(organization).Any(a => a.MemberNames.Contains("Name") && a.ErrorMessage.Contains("Name must be set!")));
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