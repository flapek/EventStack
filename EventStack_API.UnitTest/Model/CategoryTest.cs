using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.Model
{
    public class CategoryTest
    {
        public Category categoryModel;
        [SetUp]
        public void SetUp() => categoryModel = new Category();

        [Test]
        public void Category_IsNameRequired_False()
        {
            categoryModel.Name = null;
            Assert.IsFalse((categoryModel as object).isValid("Name", "Name must be set!"));
        }

        [Test]
        public void Category_IsNameHasMaximumOfCharacters_False()
        {
            categoryModel.Name = new string('*', 51);
            Assert.IsFalse((categoryModel as object).isValid("Name", "The maximum number"));
        }
    }
}