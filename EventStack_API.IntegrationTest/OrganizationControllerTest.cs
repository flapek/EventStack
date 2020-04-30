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
using EventStack_API.Workers;

namespace EventStack_API.IntegrationTest
{
    class OrganizationControllerTest
    {
        private readonly string baseURL = "api/Organization/";
        private HttpClient client;
        private WebClient webClient;
        private Organization goodEvent;

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Startup>();
            webClient = new WebClient();
            client = factory.CreateClient();
            goodEvent = new Organization
            {
                Name = "CompanyXXX",
                Description = "some description about company",
                Email = "example@example.com",
                Address = new Address
                {
                    Country = "Poland",
                    City = "Warsaw",
                    Street = "Piłsudzkiego 23/1",
                    ZipCode = "30-200",
                },
                Password = "P@$$w0rd",
                PhoneNumber = "666666666"
            };
        }

        #region Get method

        [Theory]
        public async Task Get_ByFilter_ChcekRensponseStatusCode_ReturnStatus200()
        {
            var httpRensponse = await client.GetAsync(baseURL);
            var content = JsonConvert.DeserializeObject<List<Event>>(await httpRensponse.Content.ReadAsStringAsync());
            var oneEvent = content.FirstOrDefault();
            httpRensponse.EnsureSuccessStatusCode();

            Assume.That(oneEvent != null);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Get_ById_ChcekRensponseStatusCode_ReturnStatus500()
        {
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = "",
                Name = ""
            };
            var httpRensponse = await client.GetAsync(baseURL);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        #endregion

        //#region Post method

        //[Test]
        //public async Task Post_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        //{
        //    var url = "/api/Event";
        //    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");

        //    var httpRensponse = await client.PostAsync(url, httpContent);

        //    httpRensponse.EnsureSuccessStatusCode();

        //    httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        //}

        //[Test]
        //public async Task Post_CheckRensponseStatusCodeWhenNameIsNotSet_ReturnStatus400()
        //{
        //    var url = "/api/Event";
        //    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Event()), Encoding.UTF8, "application/json");

        //    var httpRensponse = await client.PostAsync(url, httpContent);

        //    httpRensponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //}

        //[TestCase("sadnvfinoisdqwdnwoqkncionocesjoisadoisamkdnowqidnewonckoicoiocnewoinvksmocpjeionfcodsmopmowen")]
        //public async Task Post_CheckRensponseStatusCodeWhenNameIsLongerThan50_ReturnStatus400(string name)
        //{
        //    var url = "/api/Event";
        //    goodEvent.Name = name;
        //    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");

        //    var httpRensponse = await client.PostAsync(url, httpContent);

        //    httpRensponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //}

        //#endregion

        //#region Put method

        //[Theory]
        //public async Task Put_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        //{
        //    var url = baseURL + "/GetAll";
        //    var httpRensponseAll = await client.GetAsync(url);
        //    var content = JsonConvert.DeserializeObject<List<Event>>(await httpRensponseAll.Content.ReadAsStringAsync());
        //    var oneEvent = content.FirstOrDefault();
        //    httpRensponseAll.EnsureSuccessStatusCode();

        //    Assume.That(oneEvent != null);
        //    url = baseURL + "/" + oneEvent.Id;
        //    goodEvent.Description = "new description for event";
        //    goodEvent.IsCanceled = true;
        //    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");
        //    var httpRensponse = await client.PutAsync(url, httpContent);
        //    httpRensponse.EnsureSuccessStatusCode();

        //    httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        //}

        //[Theory]
        //public async Task Put_CheckRensponseContentWhenModelIsValid_ReturnNameIsCorrectChanged()
        //{
        //    goodEvent.Name = "new party";
        //    var url = baseURL + "/GetAll";
        //    var httpRensponseAll = await client.GetAsync(url);
        //    var content = JsonConvert.DeserializeObject<List<Event>>(await httpRensponseAll.Content.ReadAsStringAsync());
        //    var oneEvent = content.FirstOrDefault();
        //    httpRensponseAll.EnsureSuccessStatusCode();

        //    Assume.That(oneEvent != null);
        //    url = baseURL + "/" + oneEvent.Id;
        //    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");
        //    var httpRensponse = await client.PutAsync(url, httpContent);
        //    httpRensponse.EnsureSuccessStatusCode();

        //    var result = JsonConvert.DeserializeObject<Event>(await httpRensponse.Content.ReadAsStringAsync());
        //    Assert.AreEqual(goodEvent.Name, result.Name);
        //}

        //[TestCase("5e9d7e2e1c9d44000007a088s")]
        //[TestCase("5e9d7e2e1c9d44000007a")]
        //[TestCase("5e9d7e2e1c9d44000007@088")]
        //public async Task Put_CheckRensponseStatusCodeWhenIdIsNotValid_ReturnStatus500(string id)
        //{
        //    var url = baseURL + "/" + id;
        //    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodEvent), Encoding.UTF8, "application/json");
        //    var httpRensponse = await client.PutAsync(url, httpContent);

        //    httpRensponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        //}

        //#endregion

        //#region Delete method

        //[Theory]
        //public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnStatus200()
        //{
        //    var url = baseURL + "/GetAll";
        //    var httpRensponseAll = await client.GetAsync(url);
        //    var content = JsonConvert.DeserializeObject<List<Event>>(await httpRensponseAll.Content.ReadAsStringAsync());
        //    var oneEvent = content.FirstOrDefault();
        //    httpRensponseAll.EnsureSuccessStatusCode();

        //    Assume.That(oneEvent != null);
        //    url = baseURL + "/" + oneEvent.Id;
        //    var httpRensponse = await client.DeleteAsync(url);

        //    httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        //}

        //[Theory]
        //public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnTrue()
        //{
        //    var url = baseURL + "/GetAll";
        //    var httpRensponseAll = await client.GetAsync(url);
        //    var contentFromGet = JsonConvert.DeserializeObject<List<Event>>(await httpRensponseAll.Content.ReadAsStringAsync());
        //    var oneEvent = contentFromGet.FirstOrDefault();
        //    httpRensponseAll.EnsureSuccessStatusCode();

        //    Assume.That(oneEvent != null);
        //    url = baseURL + "/" + oneEvent.Id;
        //    var httpRensponse = await client.DeleteAsync(url);
        //    var contentFromDelete = JsonConvert.DeserializeObject<bool>(await httpRensponse.Content.ReadAsStringAsync());
        //    contentFromDelete.Should().BeTrue();
        //}

        //[TestCase("5e9d7e2e1c9d44000007a088s")]
        //[TestCase("5e9d7e2e1c9d44000007a")]
        //[TestCase("5e9d7e2e1c9d44000007@088")]
        //public async Task Delete_ChcekRensponseStatusCode_ReturnFalse(string id)
        //{
        //    var url = baseURL + "/" + id;
        //    var httpRensponse = await client.DeleteAsync(url);
        //    var content = JsonConvert.DeserializeObject<bool>(await httpRensponse.Content.ReadAsStringAsync());
        //    content.Should().BeFalse();
        //}

        //#endregion
    }
}
