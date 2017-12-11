using Microsoft.AspNetCore.Mvc;
using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;

namespace CoelhoLigeiro.WebApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CategoryRequest request)
        {
            try
            {
                var category = await categoryService.CreateAsync(request);

                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReadAsync(string description)
        {
            try
            {
                var categories = await categoryService.ReadAsync(description);

                return Ok(categories);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]CategoryRequest customer)
        {
            try
            {
                await categoryService.UpdateAsync(id, customer);

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
                await categoryService.DeleteAsync(guids);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}