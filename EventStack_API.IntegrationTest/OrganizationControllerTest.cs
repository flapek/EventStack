using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using EventStack_API.Models;
using System.Net;
using System.Text;
using EventStack_API.Workers;
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
        public async Task Get_ByFilter_ChcekRensponseStatusCodeWhenModelIsValidAndDataExistInDatabase_ReturnStatus200()
        {
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = "exampleX@exampleX.com",
                Name = "CompanyXXX"
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44382/api/Organization"),
                Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            var content = JsonConvert.DeserializeObject<Organization>(await response.Content.ReadAsStringAsync());

            Assume.That(null != content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestCase("@com")]
        [TestCase("tata1@.pl")]
        [TestCase("arkadiusz@-domena.pl")]
        [TestCase("lepkamariusz@_onet.pl")]
        [TestCase("malgosiawróblewska@kórnik.com")]
        [TestCase("tomasz_jajczyk.onet.pl")]
        [TestCase("mariusz@wp,pl")]
        public async Task Get_ByFilter_ChcekRensponseStatusCodeWhenEmailIsInvalid_ReturnStatus400(string email)
        {
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = email,
                Name = "CompanyXXX"
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44382/api/Organization"),
                Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCase("sadnvfinoisdqwdnwoqkncionocesjoisadoisamkdnowqidnewonckoicoiocnewoinvksmocpjeionfcodsmopmowensafmovmownvdnmnjsdnklmdokwqpokdpoeinjnmkmiosdjqwncmakpodwqckonijdsnoiewkmkdsoiv")]
        public async Task Get_ByFilter_ChcekRensponseStatusCodeWhenNameIsInvalid_ReturnStatus400(string name)
        {
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = "example@example.com",
                Name = name
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44382/api/Organization"),
                Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region Post method

        [TestCase("CompanyAAA", "exampleA@example.com")]
        [TestCase("CompanyBBB", "exampleB@example.com")]
        [TestCase("CompanyXXX", "exampleX@example.com")]
        [TestCase("CompanyYYY", "exampleY@example.com")]
        [TestCase("CompanyZZZ", "exampleZ@example.com")]
        public async Task Post_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200(string name, string email)
        {
            goodOrganization.Name = name;
            goodOrganization.Email = email;

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

        #region Put method

        [Theory]
        public async Task Put_CheckRensponseStatusCodeWhenModelIsValid_ReturnStatus200()
        {
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = "exampleX@exampleX.com",
                Name = "CompanyXXX"
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44382/api/Organization"),
                Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            var content = JsonConvert.DeserializeObject<Organization>(await response.Content.ReadAsStringAsync());

            Assume.That(null != content);

            content.Description = "new description";
            content.Password = "P@$$w0rd";
            response = await httpClient.PutAsync(baseURL + content.Id, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [TestCase("CompanyXYZ", "exampleXYZ@exampleXYZ.com", "CompanyXXX", "exampleX@exampleX.com")]
        [TestCase("CompanyXXX", "exampleX@exampleX.com", "CompanyXYZ", "exampleXYZ@exampleXYZ.com")]
        public async Task Put_CheckRensponseContentWhenModelIsValid_ReturnNameIsCorrectChanged(string expectedName, string expectedEmail, string name, string email)
        {
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = email,
                Name = name
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44382/api/Organization"),
                Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            var content = JsonConvert.DeserializeObject<Organization>(await response.Content.ReadAsStringAsync());

            Assume.That(null != content);

            content.Name = expectedName;
            content.Email = expectedEmail;
            content.Password = "P@$$w0rd";
            response = await httpClient.PutAsync(baseURL + content.Id, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
            content = JsonConvert.DeserializeObject<Organization>(await response.Content.ReadAsStringAsync());

            content.Name.Should().Be(expectedName);
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Put_CheckRensponseStatusCodeWhenIdIsNotValid_ReturnStatus500(string id)
        {
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = "exampleX@exampleX.com",
                Name = "CompanyXXX"
            };

            var response = await httpClient.PutAsync(baseURL + id, new StringContent(JsonConvert.SerializeObject(new Organization()), Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region Delete method

        [Theory]
        public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnStatus200()
        {
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = "exampleX@exampleX.com",
                Name = "CompanyXXX"
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44382/api/Organization"),
                Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            var content = JsonConvert.DeserializeObject<Organization>(await response.Content.ReadAsStringAsync());

            Assume.That(null != content);

            content.Description = "new description";
            content.Password = "P@$$w0rd";
            response = await httpClient.DeleteAsync(baseURL + content.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        public async Task Delete_CheckRensponseStatusCodeWhenIdIsCorrect_ReturnTrue()
        {
            Organization_MongoRepository.Filter filter = new Organization_MongoRepository.Filter
            {
                Email = "exampleX@exampleX.com",
                Name = "CompanyXXX"
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44382/api/Organization"),
                Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            var content = JsonConvert.DeserializeObject<Organization>(await response.Content.ReadAsStringAsync());

            Assume.That(null != content);

            content.Description = "new description";
            content.Password = "P@$$w0rd";
            response = await httpClient.DeleteAsync(baseURL + content.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentFromDelete = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            contentFromDelete.Should().BeTrue();
        }

        [TestCase("5e9d7e2e1c9d44000007a088s")]
        [TestCase("5e9d7e2e1c9d44000007a")]
        [TestCase("5e9d7e2e1c9d44000007@088")]
        public async Task Delete_ChcekRensponseStatusCodeWhenIdIsInvalid_ReturnFalse(string id)
        {
            var url = baseURL + id;
            var httpResponse = await httpClient.DeleteAsync(url);
            var content = JsonConvert.DeserializeObject<bool>(await httpResponse.Content.ReadAsStringAsync());
            content.Should().BeFalse();
        }

        #endregion
    }
}
