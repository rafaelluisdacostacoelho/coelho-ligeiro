using Microsoft.AspNetCore.Mvc;
using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using System;
using System.Collections.Generic;

namespace CoelhoLigeiro.WebApi.Controllers
{
    [Route("api/contacts")]
    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpPost]
        public void Create([FromBody]ContactRequest model)
        {
            contactService.Create(model);
        }

        [HttpGet]
        public IEnumerable<ContactResponse> Read()
        {
            return contactService.Read();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]ContactRequest model)
        {
            contactService.Update(id, model);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            contactService.Delete(id);
        }

        [HttpGet("{id}")]
        public ContactResponse GetById(Guid id)
        {
            return contactService.GetById(id);
        }
    }
}