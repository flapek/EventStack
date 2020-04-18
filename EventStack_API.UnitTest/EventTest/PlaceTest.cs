using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.EventTest
{
    public class PlaceTest
    {
        private Event eventModel;

        [SetUp]
        public void SetUp() => eventModel = new Event();

        [Test]
        public void Event_IsPlaceRequired_False()
        {
            eventModel.Place = null;
            Assert.IsFalse((eventModel as object).isValid("Place", "Place for event must be set"));
        }
    }
}