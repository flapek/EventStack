using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EventStack_API.Models;
using EventStack_API.Interfaces;
using EventStack_API.Workers;

namespace EventStack_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private Organization_MongoRepository repository { get; set; }

        public OrganizationController(IRepositoryFactory<Organization> repository)
        {
            this.repository = (Organization_MongoRepository)repository;
        }

        // GET: api/Organization/id
        [HttpGet("{id}")]
        public OrganizationWithoutImportantData Get(Organization_MongoRepository.Filter filter)
            => repository.Find(filter);

        // POST: api/Organization
        [HttpPost]
        public bool Post(Organization organizaction)
            => ModelState.IsValid ? repository.Insert(organizaction) : false;

        // PUT: api/Organization
        [HttpPut("{id}")]
        public Organization Put(string id, Organization organization)
            => ModelState.IsValid ? repository.Update(id, organization) : null;

        // DELETE: api/Organization
        [HttpDelete("{id}")]
        public bool Delete(string id)
            => repository.Delete(id);
    }
}
