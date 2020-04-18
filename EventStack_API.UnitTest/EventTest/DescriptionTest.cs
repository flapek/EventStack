using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.EventTest
{
    public class DescriptionTest
    {
        private Event eventModel;

        [SetUp]
        public void SetUp() => eventModel = new Event();

        [Test]
        public void Event_IsDescriptionRequired_False()
        {
            eventModel.Description = null;
            Assert.IsFalse((eventModel as object).isValid("Description", "Event must have"));
        }

        [Test]
        public void Event_IsDescriptionCanBeNotNull_True()
        {
            eventModel.Description = "not null";
            Assert.IsTrue((eventModel as object).isValid("Description", "Event must have"));
        }

        [Test]
        public void Event_IsDescriptionHasMaximumOfCharacters_False()
        {
            eventModel.Description = new string('*', 1001);
            Assert.IsFalse((eventModel as object).isValid("Description", "The maximum number"));
        }
    }
}