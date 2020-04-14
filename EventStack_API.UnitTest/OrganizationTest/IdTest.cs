using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class IdTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();

        [Test]
        public void Organization_IsIdRequired_False()
        {
            organization.Id = null;
            Assert.IsFalse((organization as object).isValid("Id", "Id must be defined!"));
        }

        [Test]
        public void Organization_IsIdCanBeNotNull_True()
        {
            organization.Id = "not null";
            Assert.IsTrue((organization as object).isValid("Id", "Id must be defined!"));
        }
    }
}