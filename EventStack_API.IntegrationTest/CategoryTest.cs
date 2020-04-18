using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using EventStack_API.Models;

namespace EventStack_API.IntegrationTest
{
    public class CategoryTest
    {
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Startup>();
            client = factory.CreateClient();
        }

        [Test]
        public async Task Test1()
        {
            var url = "/api/Category";
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Category
            {
                Name = "Holidays"
            }));

            var httpRensponse = await client.PostAsync(url, httpContent);

            httpRensponse.EnsureSuccessStatusCode();

            Assert.IsTrue(httpRensponse.IsSuccessStatusCode);

        }

        [Test]
        public async Task Test2()
        {
            var url = "/api/Category";
            var httpRensponse = await client.GetAsync(url);

            httpRensponse.EnsureSuccessStatusCode();

            Assert.IsTrue(httpRensponse.IsSuccessStatusCode);

        }
    }
}