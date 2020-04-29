using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EventStack_API.Models;
using EventStack_API.Interfaces;

namespace EventStack_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
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
        [HttpGet("{id}")]
        public Organization Get(string id)
            => repository.Find(id);

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
