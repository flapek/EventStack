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

        #region Country

        [Test]
        public void Address_IsCountryRequired_False()
        {
            addressModel.Country = null;
            Assert.IsFalse((addressModel as object).isValid("Country", "Country must be set!"));
        }

        #endregion

        [Test]
        public void Address_IsCityRequired_False()
        {
            addressModel.City = null;
            Assert.IsFalse((addressModel as object).isValid("City", "City must be set!"));
        }

        #region Street

        [Test]
        public void Address_IsStreetRequired_False()
        {
            addressModel.Street = null;
            Assert.IsFalse((addressModel as object).isValid("Street", "Street must be set!"));
        }

        [TestCase("Piłsudskiego 4/7/8")]
        [TestCase("Mazańcowicka")]
        [TestCase("Kolorowa 4b")]
        [TestCase("Przeźroczysta 4/5b")]
        [TestCase("Jana Pawła 2")]
        [TestCase("2048")] // Exist street unnamed
        public void Address_IsRegexAcceptStreet_True(string street)
        {
            addressModel.Street = street;
            Assert.IsTrue((addressModel as object).isValid("Street", "Street must contain"));
        }

        [TestCase("9 Janusza")]
        [TestCase("_Bolesława")]
        [TestCase("!@#$%^&")]
        public void Address_IsRegexRejectStreet_False(string street)
        {
            addressModel.Street = street;
            Assert.IsFalse((addressModel as object).isValid("Street", "Street must contain"));
        }

        #endregion

        #region ZipCode

        [TestCase("23-342")]
        public void Address_IsRegexAcceptZipCode_True(string zipCode)
        {
            addressModel.ZipCode = zipCode;
            Assert.IsTrue((addressModel as object).isValid("ZipCode", "ZipCode must contain"));
        }

        [TestCase("233-32")]
        [TestCase("xx-xxx")]
        [TestCase("__-___")]
        public void Address_IsRegexRejectZipCode_False(string zipCode)
        {
            addressModel.ZipCode = zipCode;
            Assert.IsFalse((addressModel as object).isValid("ZipCode", "ZipCode must contain"));
        }

        #endregion
    }
}