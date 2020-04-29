using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using EventStack_API.Models;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
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