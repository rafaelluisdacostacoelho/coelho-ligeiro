using Microsoft.AspNetCore.Mvc;
using NosEmpreendedores.Application.Interfaces;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NosEmpreendedores.WebApi.Controllers
{
    [Route("api/addresses")]
    public class AddressController : Controller
    {
        private readonly IAddressService addressService;

        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpPost]
        public IActionResult Create([FromBody, Required]AddressRequest address)
        {
            if (address == null)
            {
                return BadRequest();
            }

            addressService.Create(address);

            return Ok();
        }

        [HttpGet]
        public IEnumerable<AddressResponse> Read()
        {
            return addressService.Read();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]AddressRequest address)
        {
            addressService.Update(id, address);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            addressService.Delete(id);
        }

        [HttpGet("{id}")]
        public AddressResponse GetById(Guid id)
        {
            return addressService.GetById(id);
        }
    }
}