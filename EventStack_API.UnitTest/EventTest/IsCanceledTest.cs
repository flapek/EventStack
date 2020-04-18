using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.EventTest
{
    public class IsCanceledTest
    {
        private Event eventModel;

        [SetUp]
        public void SetUp() => eventModel = new Event();
        // 
        [Test]
        public void Event_IsCanceledRequired_False()
        {
            Assert.IsFalse((eventModel as object).isValid("IsCanceled", "Flag must be set!"));
        }
    }
}