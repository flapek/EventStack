using System.Collections.Generic;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using EventStack_API.Workers;
using Microsoft.AspNetCore.Mvc;

namespace EventStack_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private Event_MongoRepository repository { get; set; }

        public EventController(IRepositoryFactory<Event> repository)
        {
            this.repository = (Event_MongoRepository)repository;
        }

        // GET: api/Event/id
        [HttpGet("{id}")]
        public Event Get(string id)
            => repository.Find(id);

        [HttpGet]
        public IEnumerable<Event> Get()
            => repository.Find();

        // GET: api/Event
        [HttpGet]
        public Event Get(Event organization)
            => repository.Find(organization);

        // GET: api/Event
        [HttpGet]
        public IEnumerable<Event> Get(Event_MongoRepository.Filter filter)
            => repository.Find(filter);

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
        public bool Put(string id, Event organization)
            => ModelState.IsValid ? repository.Update(id, organization) : false;

        // DELETE: api/Event
        [HttpDelete("{id}")]
        public bool Delete(string id)
            => repository.Delete(id);

        // DELETE: api/Event
        [HttpDelete("{id}")]
        public bool Delete(Event organization)
            => repository.Delete(organization);

    }
}