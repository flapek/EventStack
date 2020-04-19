using System.Collections.Generic;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using EventStack_API.Workers;
using Microsoft.AspNetCore.Mvc;

namespace EventStack_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private Category_MongoRepository repository { get; set; }

        public CategoryController(IRepositoryFactory<Category> repository)
        {
            this.repository = (Category_MongoRepository)repository;
        }

        // GET: api/Category
        [HttpGet]
        public IEnumerable<Category> Get()
            => repository.Find();

        // GET: api/Category/id
        [HttpGet("{id}")]
        public Category Get(string id)
            => repository.Find(id);

        // POST: api/Category
        [HttpPost]
        public bool Post(Category organizaction)
            => ModelState.IsValid ? repository.Insert(organizaction) : false;

        // PUT: api/Category
        [HttpPut("{id}")]
        public bool Put(string id, Category organization)
            => ModelState.IsValid ? repository.Update(id, organization) : false;

        // DELETE: api/Category
        [HttpDelete("{id}")]
        public bool Delete(string id)
            => repository.Delete(id);
    }
}
