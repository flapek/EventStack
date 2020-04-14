using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class NIPTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();

        [TestCase("1234567890")]
        public void Organization_IsRegexAcceptNIP_True(string NIP)
        {
            organization.NIP = NIP;
            Assert.IsTrue((organization as object).isValid("NIP", "NIP must contain"));
        }

        [TestCase("a23456789b")]
        public void Organization_IsRegexRejectNIP_False(string NIP)
        {
            organization.NIP = NIP;
            Assert.IsFalse((organization as object).isValid("NIP", "NIP must contain"));
        }
    }
}