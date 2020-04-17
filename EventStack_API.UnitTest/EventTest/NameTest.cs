using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.EventTest
{
    public class NameTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();
    }
}