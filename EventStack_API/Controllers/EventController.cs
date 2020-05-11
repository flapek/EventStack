using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private Event_MongoRepository repositoryEvent { get; set; }

        public EventController(IRepositoryFactory<Event> repositoryEvent) => this.repositoryEvent = (Event_MongoRepository)repositoryEvent;

        // GET: api/Event/GetAll
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Event>> Get()
           => await repositoryEvent.FindAsync();

        // GET: api/Event/GetById/id
        [HttpGet("GetById/{id}")]
        public async Task<Event> Get(string id)
            => await repositoryEvent.FindAsync(id);

        // GET: api/Event/GetByFilter
        [HttpGet("GetByFilter")]
        public async Task<IEnumerable<Event>> Get(Event_MongoRepository.Filter filter)
            => await repositoryEvent.FindAsync(filter);

        // POST: api/Event/{secret}
        [HttpPost("{secret}")]
        public async Task<bool> Post([FromRoute]string secret, [FromBody]Event @event)
            => ModelState.IsValid ? await repositoryEvent.InsertAsync(secret, @event) : false;

        // PUT: api/Event/{id}/{secret}
        [HttpPut("{id}/{secret}")]
        public async Task<Event> Put([FromRoute]string id, [FromHeader]string secret, [FromBody]Event @event)
            => ModelState.IsValid ? await repositoryEvent.UpdateAsync(id, secret, @event) : null;

        // DELETE: api/Event/{id}/{secret}
        [HttpDelete("{id}")]
        public async Task<bool> Delete([FromRoute]string id, [FromRoute]string secret)
            => await repositoryEvent.Delete(id, secret);
    }
}