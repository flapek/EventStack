using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.EventTest
{
    public class NameTest
    {
        private Event eventModel;
        [SetUp]
        public void SetUp() => eventModel = new Event();

        [Test]
        public void Event_IsNameRequired_False()
        {
            eventModel.Name = null;
            Assert.IsFalse((eventModel as object).isValid("Name", "Name must be defined!"));
        }

        [Test]
        public void Event_IsNameCanBeNotNull_True()
        {
            eventModel.Name = "not null";
            Assert.IsTrue((eventModel as object).isValid("Name", "Name must be defined!"));
        }

        [Test]
        public void Event_IsNameHasMaximumOfCharacters_False()
        {
            eventModel.Name = new string('*', 51);
            Assert.IsFalse((eventModel as object).isValid("Name", "The maximum number"));
        }
    }
}