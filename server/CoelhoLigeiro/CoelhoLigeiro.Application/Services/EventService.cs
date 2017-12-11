using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Responses;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public int Create(EventRequest entity)
        {
            return eventRepository.Create(new Event
            {
                Name = entity.Name,
                Local = entity.Local,
                Begin = entity.Begin,
                End = entity.End,
                SupplierId = entity.SupplierId
            });
        }

        public IEnumerable<EventResponse> Read()
        {
            return eventRepository
                .Read()
                .Select(entity => new EventResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Local = entity.Local,
                    Begin = entity.Begin,
                    End = entity.End,
                    SupplierId = entity.SupplierId
                });
        }

        public void Update(Guid id, EventRequest entity)
        {
            eventRepository.Update(new Event
            {
                Id = id,
                Name = entity.Name,
                Local = entity.Local,
                Begin = entity.Begin,
                End = entity.End,
                SupplierId = entity.SupplierId
            });
        }

        public void Delete(Guid id)
        {
            eventRepository.Delete(id);
        }

        public EventResponse GetById(Guid id)
        {
            var entity = eventRepository.GetById(id);

            return new EventResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Local = entity.Local,
                Begin = entity.Begin,
                End = entity.End,
                SupplierId = entity.SupplierId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}