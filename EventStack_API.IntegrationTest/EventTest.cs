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
using System.Text;
using System;

namespace EventStack_API.IntegrationTest
{
    class EventTest
    {
        private readonly string baseURL = "/api/Event";
        private HttpClient client;
        private WebClient webClient;
        private Event goodEvent;

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Startup>();
            webClient = new WebClient();
            client = factory.CreateClient();
            goodEvent = new Event
            {
                Name = "Test",
                Photo = webClient.DownloadData("http://www.google.com/images/logos/ps_logo2.png"),
                StartTime = DateTime.Now.AddDays(20),
                EndTime = DateTime.Now.AddDays(22),
                PublishTime = DateTime.Now,
                Place = new Address
                {
                    City = "Warsaw",
                    Country = "Poland",
                    Street = "Test 11/4",
                    ZipCode = "43-333",
                },
                IsCanceled = false,
                Description = "some description",
                FacebookURL = "http://www.google.com",
                WebSiteURL = "http://www.google.com"
            };
        }

        #region Get method

        [Test]
        public async Task Get_All_ChcekRensponseStatusCode_ReturnStatus200()
        {
            var url = baseURL + "/GetAll";
            var httpRensponse = await client.GetAsync(url);

            httpRensponse.EnsureSuccessStatusCode();

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        public async Task Get_ById_ChcekRensponseContent_ReturnShouldNotBeNull()
        {
            var url = baseURL + "/GetAll";
            var httpRensponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Event>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneEvent = content.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);
            var httpRensponse = await client.GetAsync(baseURL + "/GetById/" + oneEvent.Id);

            httpRensponse.EnsureSuccessStatusCode();

            httpRensponse.Content.Should().NotBeNull();
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Get_ById_ChcekRensponseStatusCode_ReturnStatus500(string id)
        {
            var url = baseURL + "/GetById/" + id;
            var httpRensponse = await client.GetAsync(url);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        #endregion

        #region Post method

        [Test]
        public async Task Post_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        {
            var url = "/api/Event";
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");

            var httpRensponse = await client.PostAsync(url, httpContent);

            httpRensponse.EnsureSuccessStatusCode();

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Post_CheckRensponseStatusCodeWhenNameIsNotSet_ReturnStatus400()
        {
            var url = "/api/Event";
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Event()), Encoding.UTF8, "application/json");

            var httpRensponse = await client.PostAsync(url, httpContent);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCase("sadnvfinoisdqwdnwoqkncionocesjoisadoisamkdnowqidnewonckoicoiocnewoinvksmocpjeionfcodsmopmowen")]
        public async Task Post_CheckRensponseStatusCodeWhenNameIsLongerThan50_ReturnStatus400(string name)
        {
            var url = "/api/Event";
            goodEvent.Name = name;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");

            var httpRensponse = await client.PostAsync(url, httpContent);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region Put method

        [Theory]
        public async Task Put_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        {
            var url = baseURL + "/GetAll";
            var httpRensponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Event>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneEvent = content.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);
            url = baseURL + "/" + oneEvent.Id;
            goodEvent.Description = "new description for event";
            goodEvent.IsCanceled = true;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");
            var httpRensponse = await client.PutAsync(url, httpContent);
            httpRensponse.EnsureSuccessStatusCode();

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        public async Task Put_CheckRensponseContentWhenModelIsValid_ReturnNameIsCorrectChanged()
        {
            goodEvent.Name = "new party";
            var url = baseURL + "/GetAll";
            var httpRensponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Event>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneEvent = content.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);
            url = baseURL + "/" + oneEvent.Id;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");
            var httpRensponse = await client.PutAsync(url, httpContent);
            httpRensponse.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<Event>(await httpRensponse.Content.ReadAsStringAsync());
            Assert.AreEqual(goodEvent.Name, result.Name);
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Put_CheckRensponseStatusCodeWhenIdIsNotValid_ReturnStatus500(string id)
        {
            var url = baseURL + "/" + id;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");
            var httpRensponse = await client.PutAsync(url, httpContent);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        #endregion

        #region Delete method

        [Theory]
        public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnStatus200()
        {
            var url = baseURL + "/GetAll";
            var httpRensponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Event>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneEvent = content.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);
            url = baseURL + "/" + oneEvent.Id;
            var httpRensponse = await client.DeleteAsync(url);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnTrue()
        {
            var url = baseURL + "/GetAll";
            var httpRensponseAll = await client.GetAsync(url);
            var contentFromGet = JsonConvert.DeserializeObject<List<Event>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneEvent = contentFromGet.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);
            url = baseURL + "/" + oneEvent.Id;
            var httpRensponse = await client.DeleteAsync(url);
            var contentFromDelete = JsonConvert.DeserializeObject<bool>(await httpRensponse.Content.ReadAsStringAsync());
            contentFromDelete.Should().BeTrue();
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Delete_ChcekRensponseStatusCode_ReturnFalse(string id)
        {
            var url = baseURL + "/" + id;
            var httpRensponse = await client.DeleteAsync(url);
            var content = JsonConvert.DeserializeObject<bool>(await httpRensponse.Content.ReadAsStringAsync());
            content.Should().BeFalse();
        }

        #endregion
    }
}
