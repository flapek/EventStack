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

        [TestCase("niebieski123")]
        public void IsDigitNeeded(string password)
        {   
            var vc = new ValidationContext(organization, null, null);
            organization.Password = password;

            var errorMessage = new List<ValidationResult>();
            var result = Validator.TryValidateProperty(organization.Password, vc, errorMessage);

            Assert.IsTrue(result);
        }
    }
}