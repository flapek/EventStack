using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventStack_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IRepositoryFactory<Category> repository { get; set; }

        public CategoryController(IRepositoryFactory<Category> repository)
        {
            this.repository = repository;
        }

        // GET: api/Category/id
        [HttpGet("{id}", Name = "Get")]
        public Category Get(string id)
            => repository.Find(id);

        // GET: api/Category
        [HttpGet]
        public Category Get(Category organization)
            => repository.Find(organization);

        // GET: api/Category
        [HttpGet]
        public IEnumerable<Category> Get(IEnumerable<Category> organization)
            => repository.Find(organization);

        // POST: api/Category
        [HttpPost]
        public bool Post(Category organizaction)
            => ModelState.IsValid ? repository.Insert(organizaction) : false;

        // POST: api/Category
        [HttpPost]
        public bool Post(IEnumerable<Category> organizactions)
            => ModelState.IsValid ? repository.Insert(organizactions) : false;

        // PUT: api/Category
        [HttpPut("{id}")]
        public bool Put(Category organization)
            => ModelState.IsValid ? repository.Update(organization) : false;

        // PUT: api/Category
        [HttpPut("{id}")]
        public bool Put(IEnumerable<Category> organizations)
            => ModelState.IsValid ? repository.Update(organizations) : false;

        // DELETE: api/Category
        [HttpDelete("{id}")]
        public bool Delete(string id)
            => repository.Delete(id);

        // DELETE: api/Category
        [HttpDelete("{id}")]
        public bool Delete(Category organization)
            => repository.Delete(organization);

        // DELETE: api/Category
        [HttpDelete("{id}")]
        public bool Delete(IEnumerable<Category> organizations)
            => repository.Delete(organizations);
    }
}