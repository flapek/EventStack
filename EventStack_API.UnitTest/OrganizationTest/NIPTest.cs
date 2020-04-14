using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_API.Models;
using NUnit.Framework;

namespace EventStack_API.UnitTest.OrganizationTest
{
    public class NIPTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp()
        {
            organization = new Organization();
        }
    }
}