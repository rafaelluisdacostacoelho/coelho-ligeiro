using Microsoft.AspNetCore.Mvc;
using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using System;
using System.Collections.Generic;

namespace CoelhoLigeiro.WebApi.Controllers
{
    [Route("api/meetings")]
    public class MeetingController : Controller
    {
        private readonly IMeetingService meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            this.meetingService = meetingService;
        }

        [HttpPost]
        public void Create([FromBody]MeetingRequest model)
        {
            meetingService.Create(model);
        }

        [HttpGet]
        public IEnumerable<MeetingResponse> Read()
        {
            return meetingService.Read();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]MeetingRequest model)
        {
            meetingService.Update(id, model);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            meetingService.Delete(id);
        }

        [HttpGet("{id}")]
        public MeetingResponse GetById(Guid id)
        {
            return meetingService.GetById(id);
        }
    }
}