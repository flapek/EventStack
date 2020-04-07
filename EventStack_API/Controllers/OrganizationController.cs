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

        // GET: api/Organization
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Organization/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Organization
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

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
