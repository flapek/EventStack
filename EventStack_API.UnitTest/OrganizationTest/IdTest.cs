using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class IdTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp()
        {
            organization = new Organization();
        }

        [Test]
        public void Organization_IsIdRequired_True()
        {
            organization.Id = null;
            Assert.IsTrue(ValidateModel(organization).Any(a => a.MemberNames.Contains("Id") && a.ErrorMessage.Contains("Id must be defined!")));
        }

        [Test]
        public void Organization_IsIdCanBeNotNull_False()
        {
            organization.Id = "not null";
            Assert.IsFalse(ValidateModel(organization).Any(a => a.MemberNames.Contains("Id") && a.ErrorMessage.Contains("Id must be defined!")));
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