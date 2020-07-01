using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using EventStack_API.Models;
using EventStack_API.Workers;
using EventStack_API.Interfaces;
using System;
using System.Net;

namespace EventStack_API.Controllers
{
    public class HomeController : Controller
    {
        private Organization_MongoRepository repositoryOrganization { get; set; }
        private Event_MongoRepository repositoryEvent { get; set; }
        private Category_MongoRepository repositoryCategory { get; set; }


        public HomeController(IRepositoryFactory<Organization> repositoryOrganization, IRepositoryFactory<Event> repositoryEvent)
        {
            this.repositoryOrganization = (Organization_MongoRepository)repositoryOrganization;
            this.repositoryEvent = (Event_MongoRepository)repositoryEvent;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddOrganization()
        {
            Organization goodOrganization = new Organization
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

            repositoryOrganization.Insert(goodOrganization);
            
            return View();
        }

        public IActionResult AddEvent()
        {
            WebClient webClient = new WebClient();
            Event goodEvent = new Event
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

            repositoryEvent.Insert(goodEvent);
            
            return View();
        }

    }
}