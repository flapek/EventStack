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
        private Organization_MongoRepository repositoryOrganization { get; set; }

        public EventController(IRepositoryFactory<Event> repositoryEvent, IRepositoryFactory<Organization> repositoryOrganization)
        {
            this.repositoryEvent = (Event_MongoRepository)repositoryEvent;
            this.repositoryOrganization = (Organization_MongoRepository)repositoryOrganization;
        }

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
        public bool Post([FromRoute]string secret, [FromBody]Event @event)
            => ModelState.IsValid ? repositoryEvent.Insert(secret, @event) : false;
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var organization = repositoryOrganization.Find(secret);
        //        if (@event != null)
        //        {
        //            repositoryEvent.Insert(@event);
        //            organization.EventsId.Append(repositoryEvent.Find(@event).Id);
        //            repositoryOrganization.Update(organization.Id, organization);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        // PUT: api/Event/{id}/{secret}
        [HttpPut("{id}/{secret}")]
        public Event Put([FromRoute]string id, [FromHeader]string secret, [FromBody]Event @event)
            => ModelState.IsValid ? repositoryEvent.Update(id, secret, @event) : null;
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var organization = repositoryOrganization.Find(secret);
        //        if (@event != null)
        //        {
        //            organization.EventsId.TakeWhile(x => x == @event.Id);
        //            repositoryEvent.Insert(@event);
        //            organization.EventsId.Append(repositoryEvent.Find(@event).Id);
        //            repositoryOrganization.Update(organization.Id, organization);
        //            return repositoryEvent.Update(id, @event);
        //        }
        //    }
        //    return null;
        //}

        // DELETE: api/Event/{id}/{secret}
        [HttpDelete("{id}")]
        public bool Delete([FromRoute]string id, [FromRoute]string secret)
            => repositoryEvent.Delete(id, secret);
        //{
        //    var organization = repositoryOrganization.Find(secret);
        //    organization.EventsId.TakeWhile(x => x == id);
        //    repositoryOrganization.Update(organization.Id, organization);
        //    return repositoryEvent.Delete(id);
        //}
    }
}