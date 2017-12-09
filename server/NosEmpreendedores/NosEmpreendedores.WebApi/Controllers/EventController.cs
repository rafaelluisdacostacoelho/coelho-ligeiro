using Microsoft.AspNetCore.Mvc;
using NosEmpreendedores.Application.Interfaces;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;
using System;
using System.Collections.Generic;

namespace NosEmpreendedores.WebApi.Controllers
{
    [Route("api/events")]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpPost]
        public void Create([FromBody]EventRequest model)
        {
            eventService.Create(model);
        }

        [HttpGet]
        public IEnumerable<EventResponse> Read()
        {
            return eventService.Read();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]EventRequest model)
        {
            eventService.Update(id, model);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            eventService.Delete(id);
        }

        [HttpGet("{id}")]
        public EventResponse GetById(Guid id)
        {
            return eventService.GetById(id);
        }
    }
}