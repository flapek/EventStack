using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.Model
{
    public class OrganizationTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();
        
        #region Description

        [Test]
        public void Organization_IsDescriptionHasMaximumOfCharacters_False()
        {
            organization.Destription = new string('*', 1001);
            Assert.IsFalse((organization as object).isValid("Destription", "The maximum number"));
        }

        #endregion
    }
}