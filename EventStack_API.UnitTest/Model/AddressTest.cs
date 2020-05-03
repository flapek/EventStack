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

        #region Street

        [TestCase("Piłsudskiego 4/7/8")]
        [TestCase("Mazańcowicka")]
        [TestCase("Kolorowa 4b")]
        [TestCase("Przeźroczysta 4/5b")]
        public void Address_IsRegexAcceptStreet_True(string street)
        {
            addressModel.Street = street;
            Assert.IsTrue((addressModel as object).isValid("Street", "Street must contain"));
        }

        #endregion
    }
}