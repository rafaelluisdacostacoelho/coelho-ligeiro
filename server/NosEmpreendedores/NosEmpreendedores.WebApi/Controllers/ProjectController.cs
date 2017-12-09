using Microsoft.AspNetCore.Mvc;
using NosEmpreendedores.Application.Interfaces;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;
using System;
using System.Collections.Generic;

namespace NosEmpreendedores.WebApi.Controllers
{
    [Route("api/projects")]
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpPost]
        public void Create([FromBody]ProjectRequest model)
        {
            projectService.Create(model);
        }

        [HttpGet]
        public IEnumerable<ProjectResponse> Read()
        {
            return projectService.Read();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]ProjectRequest model)
        {
            projectService.Update(id, model);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            projectService.Delete(id);
        }

        [HttpGet("{id}")]
        public ProjectResponse GetById(Guid id)
        {
            return projectService.GetById(id);
        }
    }
}