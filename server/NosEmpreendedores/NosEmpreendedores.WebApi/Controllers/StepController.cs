using Microsoft.AspNetCore.Mvc;
using NosEmpreendedores.Application.Interfaces;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;
using System;
using System.Collections.Generic;

namespace NosEmpreendedores.WebApi.Controllers
{
    [Route("api/steps")]
    public class StepController : Controller
    {
        private readonly IStepService stepService;

        public StepController(IStepService stepService)
        {
            this.stepService = stepService;
        }

        [HttpPost]
        public void Create([FromBody]StepRequest model)
        {
            stepService.Create(model);
        }

        [HttpGet]
        public IEnumerable<StepResponse> Read()
        {
            return stepService.Read();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]StepRequest model)
        {
            stepService.Update(id, model);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            stepService.Delete(id);
        }

        [HttpGet("{id}")]
        public StepResponse GetById(Guid id)
        {
            return stepService.GetById(id);
        }        
    }
}