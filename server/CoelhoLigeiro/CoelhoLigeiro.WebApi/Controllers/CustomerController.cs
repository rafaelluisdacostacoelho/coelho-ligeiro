using Microsoft.AspNetCore.Mvc;
using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace CoelhoLigeiro.WebApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CustomerRequest request)
        {
            try
            {
                var customer = await customerService.CreateAsync(request);

                return Ok(customer);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReadAsync(CustomerFilterRequest request)
        {
            try
            {
                var customers = await customerService.ReadAsync(request);

                return Ok(customers);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]CustomerRequest request)
        {
            try
            {
                await customerService.UpdateAsync(id, request);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid[] guids)
        {
            try
            {
                await customerService.DeleteAsync(guids);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid guid)
        {
            try
            {
                var customer = await customerService.GetByIdAsync(guid);

                return Ok(customer);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
