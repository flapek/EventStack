using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest
{
    public class EndTimeTest
    {
        private Event eventModel;

        [SetUp]
        public void SetUp() => eventModel = new Event();
    }
}