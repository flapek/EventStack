﻿using System.Collections.Generic;
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
        private Event_MongoRepository repository { get; set; }

        public EventController(IRepositoryFactory<Event> repository)
        {
            this.repository = (Event_MongoRepository)repository;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<IEnumerable<Event>> Get()
           => await repository.FindAsync();

        // GET: api/Event/GetById/id
        [HttpGet("GetById/{id}")]
        public async Task<Event> Get(string id)
            => await repository.FindAsync(id);

        //TODO zastanowić się czy potrzebne
        //// GET: api/Event
        //[HttpGet]
        //public Event Get(Event organization)
        //    => repository.Find(organization);

        // GET: api/Event/GetByFilter
        [HttpGet("GetByFilter")]
        public async Task<IEnumerable<Event>> Get(Event_MongoRepository.Filter filter)
            => await repository.FindAsync(filter);

        // POST: api/Event
        [HttpPost]
        public bool Post(Event organizaction)
            => ModelState.IsValid ? repository.Insert(organizaction) : false;

        //TODO zastanowić się czy potrzebne
        //// POST: api/Event
        //[HttpPost]
        //public bool Post(IEnumerable<Event> organizactions)
        //    => ModelState.IsValid ? repository.Insert(organizactions) : false;

        // PUT: api/Event
        [HttpPut("{id}")]
        public Event Put(string id, Event organization)
            => ModelState.IsValid ? repository.Update(id, organization) : null;

        // DELETE: api/Event
        [HttpDelete("{id}")]
        public bool Delete(string id)
            => repository.Delete(id);
    }
}