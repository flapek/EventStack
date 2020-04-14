using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;
using System;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class DestriptionTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();

        [Test]
        public void Organization_IsDestriptionHasMaximumOfCharacters_False()
        {
            organization.Destription = new string('*', 1001);
            Assert.IsFalse((organization as object).isValid("Destription", "The maximum number"));
        }
    }
}