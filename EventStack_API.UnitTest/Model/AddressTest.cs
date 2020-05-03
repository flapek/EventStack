using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.Model
{
    public class AddressTest
    {
        private Address addressModel;

        [SetUp]
        public void SetUp() => addressModel = new Address();

        public void Address_IsRegexAcceptStreet_True(string street)
        {
            addressModel.Street = street;
            Assert.IsTrue((addressModel as object).isValid("Street", "Street must contain"));
        }
    }
}