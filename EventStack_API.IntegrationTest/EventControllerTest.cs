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
    class EventControllerTest
    {
        private readonly string baseURL = "/api/Event/";
        private readonly string secret = "00000000-0000-0000-0000-000000000000";
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
            var url = baseURL + "GetAll";
            var httpResponse = await client.GetAsync(url);

            httpResponse.EnsureSuccessStatusCode();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        public async Task Get_ById_ChcekRensponseContent_ReturnShouldNotBeNull()
        {
            var url = baseURL + "GetAll";
            var httpResponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Event>>(await httpResponseAll.Content.ReadAsStringAsync());
            var oneEvent = content.FirstOrDefault();
            httpResponseAll.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);
            var httpResponse = await client.GetAsync(baseURL + "GetById/" + oneEvent.Id);

            httpResponse.EnsureSuccessStatusCode();

            httpResponse.Content.Should().NotBeNull();
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Get_ById_ChcekRensponseStatusCode_ReturnStatus500(string id)
        {
            var url = baseURL + "GetById/" + id;
            var httpResponse = await client.GetAsync(url);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        #endregion

        #region Post method

        [Test]
        public async Task Post_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(baseURL + secret, httpContent);

            httpResponse.EnsureSuccessStatusCode();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Post_CheckRensponseStatusCodeWhenNameIsNotSet_ReturnStatus400()
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Event()), Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(baseURL + secret, httpContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCase("sadnvfinoisdqwdnwoqkncionocesjoisadoisamkdnowqidnewonckoicoiocnewoinvksmocpjeionfcodsmopmowen")]
        public async Task Post_CheckRensponseStatusCodeWhenNameIsLongerThan50_ReturnStatus400(string name)
        {
            goodEvent.Name = name;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(baseURL + secret, httpContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region Put method

        [Theory]
        public async Task Put_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        {
            var url = baseURL + "GetAll";
            var httpResponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Event>>(await httpResponseAll.Content.ReadAsStringAsync());
            var oneEvent = content.FirstOrDefault();
            httpResponseAll.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);
            url = baseURL + oneEvent.Id + "/" + secret;
            goodEvent.Description = "new description for event";
            goodEvent.IsCanceled = true;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(url, httpContent);
            httpResponse.EnsureSuccessStatusCode();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        public async Task Put_CheckRensponseContentWhenModelIsValid_ReturnNameIsCorrectChanged()
        {
            goodEvent.Name = "new party";
            var url = baseURL + "GetAll";
            var httpResponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Event>>(await httpResponseAll.Content.ReadAsStringAsync());
            var oneEvent = content.FirstOrDefault();
            httpResponseAll.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);
            url = baseURL + oneEvent.Id + "/" + secret;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(url, httpContent);
            httpResponse.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<Event>(await httpResponse.Content.ReadAsStringAsync());
            Assert.AreEqual(goodEvent.Name, result.Name);
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Put_CheckRensponseStatusCodeWhenIdNotFound_ReturnStatus204(string id)
        {
            var url = baseURL + id + "/" + secret;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(url, httpContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion

        #region Delete method

        [Theory]
        public async Task Delete_CheckRensponseStatusCode_ReturnStatus200()
        {
            var url = baseURL + "GetAll";
            var httpResponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Event>>(await httpResponseAll.Content.ReadAsStringAsync());
            var oneEvent = content.LastOrDefault();
            httpResponseAll.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);
            url = baseURL + oneEvent.Id + "/" + secret;
            var httpResponse = await client.DeleteAsync(url);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Delete_ChcekRensponseStatusCode_ReturnFalse(string id)
        {
            var url = baseURL + id + "/" + secret;
            var httpResponse = await client.DeleteAsync(url);
            var content = JsonConvert.DeserializeObject<bool>(await httpResponse.Content.ReadAsStringAsync());
            content.Should().BeFalse();
        }

        #endregion
    }
}
