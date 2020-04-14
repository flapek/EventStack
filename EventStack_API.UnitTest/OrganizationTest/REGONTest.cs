using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class REGONTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();

        [TestCase("123456789")]
        public void Organization_IsRegexAcceptREGON_True(string REGON)
        {
            organization.REGON = REGON;
            Assert.IsTrue((organization as object).isValid("REGON", "REGON must contain"));
        }

        [TestCase("a2345678b")]
        public void Organization_IsRegexRejectREGON_False(string REGON)
        {
            organization.REGON = REGON;
            Assert.IsFalse((organization as object).isValid("REGON", "REGON must contain"));
        }
    }
}