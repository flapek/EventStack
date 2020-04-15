using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EventStack_API.Models;
using EventStack_API.Interfaces;

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

        // GET: api/Organization/id
        [HttpGet("{id}", Name = "Get")]
        public Organization Get(string id)
            => repository.Find(id);

        // GET: api/Organization
        [HttpGet]
        public Organization Get(Organization organization)
            => repository.Find(organization);

        // GET: api/Organization
        [HttpGet]
        public IEnumerable<Organization> Get(IEnumerable<Organization> organization)
            => repository.Find(organization);

        // POST: api/Organization
        [HttpPost]
        public bool Post(Organization organizaction)
            => ModelState.IsValid ? repository.Insert(organizaction) : false;

        // POST: api/Organization
        [HttpPost]
        public bool Post(IEnumerable<Organization> organizactions)
            => ModelState.IsValid ? repository.Insert(organizactions) : false;

        // PUT: api/Organization
        [HttpPut("{id}")]
        public bool Put(Organization organization)
            => ModelState.IsValid ? repository.Update(organization) : false;

        // PUT: api/Organization
        [HttpPut("{id}")]
        public bool Put(IEnumerable<Organization> organizations)
            => ModelState.IsValid ? repository.Update(organizations) : false;


        // DELETE: api/Organization
        [HttpDelete("{id}")]
        public bool Delete(string id)
            => repository.Delete(id);


        // DELETE: api/Organization
        [HttpDelete("{id}")]
        public bool Delete(Organization organization)
            => repository.Delete(organization);


        // DELETE: api/Organization
        [HttpDelete("{id}")]
        public bool Delete(IEnumerable<Organization> organizations)
            => repository.Delete(organizations);
    }
}
