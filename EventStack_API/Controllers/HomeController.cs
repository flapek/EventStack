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

namespace EventStack_API.Controllers
{
    public class HomeController : Controller
    {
        private Organization_MongoRepository repository { get; set; }

        public HomeController(IRepositoryFactory<Organization> repository)
        {
            this.repository = (Organization_MongoRepository)repository;
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

            repository.Insert(goodOrganization);
            
            return View();
        }
    }
}