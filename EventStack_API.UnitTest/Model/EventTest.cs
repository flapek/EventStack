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

        #region DescriptionTest

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

        #endregion

        #region NameTest 

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

        #endregion

        #region PlaceTest

        [Test]
        public void Event_IsPlaceRequired_False()
        {
            eventModel.Place = null;
            Assert.IsFalse((eventModel as object).isValid("Place", "Place for event must be set"));
        }

        #endregion
    }
}