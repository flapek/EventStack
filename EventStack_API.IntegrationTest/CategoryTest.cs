using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using EventStack_API.Models;
using System.Net;
using System.Collections.Generic;
using System.Linq;

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

        #region Get method

        [Test]
        public async Task Get_All_ChcekRensponseStatusCode_ReturnStatus200()
        {
            var url = "/api/Category";
            var httpRensponse = await client.GetAsync(url);

            httpRensponse.EnsureSuccessStatusCode();

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        public async Task Get_ById_ChcekRensponseContent_ReturnShouldNotBeNull()
        {
            var url = "/api/Category/";
            var httpRensponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Category>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneCategory = content.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneCategory != null);
            var httpRensponse = await client.GetAsync(url + oneCategory.Id);

            httpRensponse.EnsureSuccessStatusCode();

            httpRensponse.Content.Should().NotBeNull();
        }

        [TestCase("d328hdn8s9auy3d")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Get_ById_ChcekRensponseStatusCode_ReturnStatus400(string id)
        {
            var url = "/api/Category/" + id;
            var httpRensponse = await client.GetAsync(url);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }


        #endregion

        [Test]
        public async Task post()
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
    }
}