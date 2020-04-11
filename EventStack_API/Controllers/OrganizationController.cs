using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EventStack_API.Models;
using EventStack_API.Interfaces;
using MongoDB.Bson;

namespace EventStack_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private IRepositoryFactory<Organization> repository { get; set; }

        public OrganizationController(IRepositoryFactory<Organization> repository)
        {
            this.repository = repository;
        }

        // GET: api/Organization
        [HttpGet]
        public Organization Get(Organization organization) => repository.Find(organization);

        // GET: api/Organization/5
        [HttpGet("{id}", Name = "Get")]
        public Organization Get(string id) => repository.Find(id);

        // POST: api/Organization
        [HttpPost]
        public bool Post(Organization organizaction) => repository.Insert(organizaction);

        // PUT: api/Organization/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
