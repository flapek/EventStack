using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

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