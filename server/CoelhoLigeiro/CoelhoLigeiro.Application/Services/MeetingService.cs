using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Application.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository meetingRepository;

        public MeetingService(IMeetingRepository meetingRepository)
        {
            this.meetingRepository = meetingRepository;
        }

        public int Create(MeetingRequest meeting)
        {
            return meetingRepository.Create(new Meeting
            {
                Date = meeting.Date,
                Duration = meeting.Duration,
                Local = meeting.Local,
                Description = meeting.Description,
                CustomerId = meeting.CustomerId,
                SupplierId = meeting.SupplierId
            });
        }

        public IEnumerable<MeetingResponse> Read()
        {
            return meetingRepository
                .Read()
                .Select(meeting => new MeetingResponse
                {
                    Id = meeting.Id,
                    Date = meeting.Date,
                    Duration = meeting.Duration,
                    Local = meeting.Local,
                    Description = meeting.Description,
                    Creation = meeting.Creation,
                    CustomerId = meeting.CustomerId,
                    SupplierId = meeting.SupplierId
                });
        }

        public void Update(Guid id, MeetingRequest meeting)
        {
            meetingRepository.Update(new Meeting
            {
                Id = id,
                Date = meeting.Date,
                Duration = meeting.Duration,
                Local = meeting.Local,
                Description = meeting.Description,
                CustomerId = meeting.CustomerId,
                SupplierId = meeting.SupplierId
            });
        }

        public void Delete(Guid id)
        {
            meetingRepository.Delete(id);
        }

        public MeetingResponse GetById(Guid id)
        {
            var meeting = meetingRepository.GetById(id);

            return new MeetingResponse
            {
                Id = meeting.Id,
                Date = meeting.Date,
                Duration = meeting.Duration,
                Local = meeting.Local,
                Description = meeting.Description,
                Creation = meeting.Creation,
                CustomerId = meeting.CustomerId,
                SupplierId = meeting.SupplierId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}