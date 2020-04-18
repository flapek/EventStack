using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.EventTest
{
    public class PublishTimeTest
    {
        private Event eventModel;

        [SetUp]
        public void SetUp() => eventModel = new Event();

        [Test]
        public void Event_IsPublishTimeRequired_False()
        {
            Assert.IsFalse((eventModel as object).isValid("PublishTime", "Publish date must be set"));
        }
    }
}