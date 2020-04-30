using NUnit.Framework;
using System.Threading.Tasks;
using EventStack_API.Models;
using System.Net;
namespace EventStack_API.UnitTest.Model
{
    class AddressTest
    {
        private Address address;
        private WebClient webClient;

        [SetUp]
        public void Setup()
        {
            address = new Address()
            {
                Country = "Polska",
                City = "Bielsko-Biała",
                Street = "",
                ZipCode = "43-300"
            };
            webClient = new WebClient();
        }

        [Test]
        public async Task SetLocationBasedOnAddres()
        {
            
        }

    }
}

//AIzaSyDkm2cElE3pG8PapgmHLU_ZxnqD8Bk0pmw - apiKey