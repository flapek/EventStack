using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.Model
{
    public class EventTest
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
    }
}