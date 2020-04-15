using System.Collections.Generic;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventStack_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IRepositoryFactory<Event> repository { get; set; }

        public EventController(IRepositoryFactory<Event> repository)
        {
            this.repository = repository;
        }

        // GET: api/Event/id
        [HttpGet("{id}", Name = "Get")]
        public Event Get(string id)
            => repository.Find(id);

        // GET: api/Event
        [HttpGet]
        public Event Get(Event organization)
            => repository.Find(organization);

        // GET: api/Event
        [HttpGet]
        public IEnumerable<Event> Get(IEnumerable<Event> organization)
            => repository.Find(organization);

        // POST: api/Event
        [HttpPost]
        public bool Post(Event organizaction)
            => ModelState.IsValid ? repository.Insert(organizaction) : false;

        // POST: api/Event
        [HttpPost]
        public bool Post(IEnumerable<Event> organizactions)
            => ModelState.IsValid ? repository.Insert(organizactions) : false;

        // PUT: api/Event
        [HttpPut("{id}")]
        public bool Put(Event organization)
            => ModelState.IsValid ? repository.Update(organization) : false;

        // PUT: api/Event
        [HttpPut("{id}")]
        public bool Put(IEnumerable<Event> organizations)
            => ModelState.IsValid ? repository.Update(organizations) : false;

        // DELETE: api/Event
        [HttpDelete("{id}")]
        public bool Delete(string id)
            => repository.Delete(id);

        // DELETE: api/Event
        [HttpDelete("{id}")]
        public bool Delete(Event organization)
            => repository.Delete(organization);

        // DELETE: api/Event
        [HttpDelete("{id}")]
        public bool Delete(IEnumerable<Event> organizations)
            => repository.Delete(organizations);
    }
}