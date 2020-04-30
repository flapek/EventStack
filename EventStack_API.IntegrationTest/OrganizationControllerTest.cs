﻿using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using EventStack_API.Models;
using System.Net;
using System.Text;
using EventStack_API.Workers;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EventStack_API.IntegrationTest
{
    class OrganizationControllerTest
    {
        private readonly string baseURL = "api/Organization/";
        private HttpClient httpClient;
        private WebClient webClient;
        private Organization goodOrganization;

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Startup>();
            webClient = new WebClient();
            httpClient = factory.CreateClient();
            goodOrganization = new Organization
            {
                Name = "CompanyXXX",
                Description = "some description about company",
                Email = "example@example.com",
                Address = new Address
                {
                    Country = "Poland",
                    City = "Warsaw",
                    Street = "Pilsudzkiego 23/1",
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
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = "example@example.com",
                Name = "CompanyXXX"
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44382/api/Organization"),
                Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var content = JsonConvert.DeserializeObject<Organization>(await response.Content.ReadAsStringAsync());

            Assume.That(content != null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        #endregion

        #region Post method

        [Theory]
        public async Task Post_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodOrganization), Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(baseURL, httpContent);

            httpResponse.EnsureSuccessStatusCode();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Post_CheckRensponseStatusCodeWhenNameIsNotSet_ReturnStatus400()
        {
            goodOrganization.Name = string.Empty;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodOrganization), Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(baseURL, httpContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCase("sadnvfinoisdqwdnwoqkncionocesjoisadoisamkdnowqidnewonckoicoiocnewoinvksmocpjeionfcodsmopmowensafmovmownvdnmnjsdnklmdokwqpokdpoeinjnmkmiosdjqwncmakpodwqckonijdsnoiewkmkdsoiv")]
        public async Task Post_CheckRensponseStatusCodeWhenNameIsLongerThan100_ReturnStatus400(string name)
        {
            goodOrganization.Name = name;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodOrganization), Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(baseURL, httpContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCase("Password")]
        [TestCase("Passwor")]
        [TestCase("Password123")]
        [TestCase("Password\"\"123")]
        public async Task Post_CheckRensponseStatusCodeWhenPasswordIsInvalid_ReturnStatus400(string password)
        {
            goodOrganization.Password = password;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodOrganization), Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(baseURL, httpContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCase("@com")]
        [TestCase("_a@domena.net")]
        [TestCase("tata1@.pl")]
        [TestCase("arkadiusz@-domena.pl")]
        [TestCase("lepkamariusz@_onet.pl")]
        [TestCase("malgosiawróblewska@kórnik.com")]
        [TestCase("tomasz_jajczyk.onet.pl")]
        [TestCase("tomek@interia")]
        [TestCase("mariusz@wp,pl")]
        [TestCase("-gosia@domena.net")]
        [TestCase("kasia1997@pl")]
        public async Task Post_CheckRensponseStatusCodeWhenEmailIsInvalid_ReturnStatus400(string password)
        {
            goodOrganization.Password = password;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodOrganization), Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(baseURL, httpContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCase("123456789")]
        [TestCase("123456789o")]
        [TestCase("12345678901")]
        [TestCase("123456789@")]
        public async Task Post_CheckRensponseStatusCodeWhenNIPIsInvalid_ReturnStatus400(string nip)
        {
            goodOrganization.NIP = nip;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodOrganization), Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(baseURL, httpContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        //#region Put method

        //[Theory]
        //public async Task Put_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        //{
        //    var url = baseURL + "/GetAll";
        //    var httpResponseAll = await client.GetAsync(url);
        //    var content = JsonConvert.DeserializeObject<List<Organization>>(await httpResponseAll.Content.ReadAsStringAsync());
        //    var oneOrganization = content.FirstOrDefault();
        //    httpResponseAll.EnsureSuccessStatusCode();

        //    Assume.That(oneOrganization != null);
        //    url = baseURL + "/" + oneOrganization.Id;
        //    goodOrganization.Description = "new description for event";
        //    goodOrganization.IsCanceled = true;
        //    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodOrganization), Encoding.UTF8, "application/json");
        //    var httpResponse = await client.PutAsync(url, httpContent);
        //    httpResponse.EnsureSuccessStatusCode();

        //    httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        //}

        //[Theory]
        //public async Task Put_CheckRensponseContentWhenModelIsValid_ReturnNameIsCorrectChanged()
        //{
        //    goodOrganization.Name = "new party";
        //    var url = baseURL + "/GetAll";
        //    var httpResponseAll = await client.GetAsync(url);
        //    var content = JsonConvert.DeserializeObject<List<Organization>>(await httpResponseAll.Content.ReadAsStringAsync());
        //    var oneOrganization = content.FirstOrDefault();
        //    httpResponseAll.EnsureSuccessStatusCode();

        //    Assume.That(oneOrganization != null);
        //    url = baseURL + "/" + oneOrganization.Id;
        //    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodOrganization), Encoding.UTF8, "application/json");
        //    var httpResponse = await client.PutAsync(url, httpContent);
        //    httpResponse.EnsureSuccessStatusCode();

        //    var result = JsonConvert.DeserializeObject<Organization>(await httpResponse.Content.ReadAsStringAsync());
        //    Assert.AreEqual(goodOrganization.Name, result.Name);
        //}

        //[TestCase("5e9d7e2e1c9d44000007a088s")]
        //[TestCase("5e9d7e2e1c9d44000007a")]
        //[TestCase("5e9d7e2e1c9d44000007@088")]
        //public async Task Put_CheckRensponseStatusCodeWhenIdIsNotValid_ReturnStatus500(string id)
        //{
        //    var url = baseURL + "/" + id;
        //    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(goodOrganization), Encoding.UTF8, "application/json");
        //    var httpResponse = await client.PutAsync(url, httpContent);

        //    httpResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        //}

        //#endregion

        //#region Delete method

        //[Theory]
        //public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnStatus200()
        //{
        //    var url = baseURL + "/GetAll";
        //    var httpResponseAll = await client.GetAsync(url);
        //    var content = JsonConvert.DeserializeObject<List<Organization>>(await httpResponseAll.Content.ReadAsStringAsync());
        //    var oneOrganization = content.FirstOrDefault();
        //    httpResponseAll.EnsureSuccessStatusCode();

        //    Assume.That(oneOrganization != null);
        //    url = baseURL + "/" + oneOrganization.Id;
        //    var httpResponse = await client.DeleteAsync(url);

        //    httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        //}

        //[Theory]
        //public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnTrue()
        //{
        //    var url = baseURL + "/GetAll";
        //    var httpResponseAll = await client.GetAsync(url);
        //    var contentFromGet = JsonConvert.DeserializeObject<List<Organization>>(await httpResponseAll.Content.ReadAsStringAsync());
        //    var oneOrganization = contentFromGet.FirstOrDefault();
        //    httpResponseAll.EnsureSuccessStatusCode();

        //    Assume.That(oneOrganization != null);
        //    url = baseURL + "/" + oneOrganization.Id;
        //    var httpResponse = await client.DeleteAsync(url);
        //    var contentFromDelete = JsonConvert.DeserializeObject<bool>(await httpResponse.Content.ReadAsStringAsync());
        //    contentFromDelete.Should().BeTrue();
        //}

        //[TestCase("5e9d7e2e1c9d44000007a088s")]
        //[TestCase("5e9d7e2e1c9d44000007a")]
        //[TestCase("5e9d7e2e1c9d44000007@088")]
        //public async Task Delete_ChcekRensponseStatusCode_ReturnFalse(string id)
        //{
        //    var url = baseURL + "/" + id;
        //    var httpResponse = await client.DeleteAsync(url);
        //    var content = JsonConvert.DeserializeObject<bool>(await httpResponse.Content.ReadAsStringAsync());
        //    content.Should().BeFalse();
        //}

        //#endregion
    }
}
