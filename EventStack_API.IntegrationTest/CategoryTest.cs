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

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Get_ById_ChcekRensponseStatusCode_ReturnStatus500(string id)
        {
            var url = "/api/Category/" + id;
            var httpRensponse = await client.GetAsync(url);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        #endregion

        #region Post method

        [Test]
        public async Task Post_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        {
            var url = "/api/Category";
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Category
            {
                Name = "Holidays"
            }), Encoding.UTF8, "application/json");

            var httpRensponse = await client.PostAsync(url, httpContent);

            httpRensponse.EnsureSuccessStatusCode();

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Post_CheckRensponseStatusCodeWhenNameIsNotSet_ReturnStatus400()
        {
            var url = "/api/Category";
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Category()), Encoding.UTF8, "application/json");

            var httpRensponse = await client.PostAsync(url, httpContent);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCase("sadnvfinoisdqwdnwoqkncionocesjoisadoisamkdnowqidnewonckoicoiocnewoinvksmocpjeionfcodsmopmowen")]
        public async Task Post_CheckRensponseStatusCodeWhenNameIsLongerThan50_ReturnStatus400(string name)
        {
            var url = "/api/Category";
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Category
            {
                Name = name
            }), Encoding.UTF8, "application/json");

            var httpRensponse = await client.PostAsync(url, httpContent);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region Put method

        [Theory]
        public async Task Put_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        {
            var url = "/api/Category/";
            var httpRensponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Category>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneCategory = content.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneCategory != null);
            url += oneCategory.Id;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Category
            {
                Name = "changedName"
            }), Encoding.UTF8, "application/json");
            var httpRensponse = await client.PutAsync(url, httpContent);
            httpRensponse.EnsureSuccessStatusCode();

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        public async Task Put_CheckRensponseContentWhenModelIsValid_ReturnNameIsCorrectChanged()
        {
            var expected = new Category { Name = "changedName" };
            var url = "/api/Category/";
            var httpRensponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Category>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneCategory = content.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneCategory != null);
            url += oneCategory.Id;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(expected), Encoding.UTF8, "application/json");
            var httpRensponse = await client.PutAsync(url, httpContent);
            httpRensponse.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<Category>(await httpRensponse.Content.ReadAsStringAsync());
            Assert.AreEqual(expected.Name, result.Name);
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Put_CheckRensponseStatusCodeWhenIdIsNotValid_ReturnStatus500(string id)
        {
            var url = "/api/Category/" + id;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Category
            {
                Name = "changedName"
            }), Encoding.UTF8, "application/json");
            var httpRensponse = await client.PutAsync(url, httpContent);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        #endregion

        #region Delete method

        [Theory]
        public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnStatus200()
        {
            var url = "/api/Category/";
            var httpRensponseAll = await client.GetAsync(url);
            var content = JsonConvert.DeserializeObject<List<Category>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneCategory = content.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneCategory != null);
            url += oneCategory.Id;
            var httpRensponse = await client.DeleteAsync(url);

            httpRensponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnTrue()
        {
            var url = "/api/Category/";
            var httpRensponseAll = await client.GetAsync(url);
            var contentFromGet = JsonConvert.DeserializeObject<List<Category>>(await httpRensponseAll.Content.ReadAsStringAsync());
            var oneCategory = contentFromGet.FirstOrDefault();
            httpRensponseAll.EnsureSuccessStatusCode();

            Assume.That(oneCategory != null);
            url += oneCategory.Id;
            var httpRensponse = await client.DeleteAsync(url);
            var contentFromDelete = JsonConvert.DeserializeObject<bool>(await httpRensponse.Content.ReadAsStringAsync());
            contentFromDelete.Should().BeTrue();
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Delete_ChcekRensponseStatusCode_ReturnFalse(string id)
        {
            var url = "/api/Category/" + id;
            var httpRensponse = await client.DeleteAsync(url);
            var content = JsonConvert.DeserializeObject<bool>(await httpRensponse.Content.ReadAsStringAsync());
            content.Should().BeFalse();
        }

        #endregion

    }
}