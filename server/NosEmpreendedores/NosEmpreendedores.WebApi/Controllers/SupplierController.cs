using Microsoft.AspNetCore.Mvc;
using NosEmpreendedores.Application.Interfaces;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NosEmpreendedores.WebApi.Controllers
{
    [Route("api/suppliers")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]SupplierRequest request)
        {
            try
            {
                await supplierService.CreateAsync(request);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReadAsync(SupplierFilterRequest request)
        {
            try
            {
                var suppliers = await supplierService.ReadAsync(request);

                return Ok(suppliers);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]SupplierRequest request)
        {
            try
            {
                await supplierService.UpdateAsync(id, request);

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
                await supplierService.DeleteAsync(guids);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var supplier = await supplierService.GetByIdAsync(id);

                return Ok(supplier);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}