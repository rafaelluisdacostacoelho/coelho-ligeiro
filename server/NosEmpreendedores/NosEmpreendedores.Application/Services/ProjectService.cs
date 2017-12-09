using NosEmpreendedores.Application.Interfaces;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;
using NosEmpreendedores.Domain.Interfaces.Repositories;
using NosEmpreendedores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NosEmpreendedores.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public int Create(ProjectRequest project)
        {
            return projectRepository.Create(new Project
            {
                Name = project.Name,
                Description = project.Description,
                CustomerId = project.CustomerId,
                SupplierId = project.SupplierId
            });
        }

        public IEnumerable<ProjectResponse> Read()
        {
            return projectRepository
                .Read()
                .Select(project => new ProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    Creation = project.Creation,
                    CustomerId = project.CustomerId,
                    SupplierId = project.SupplierId
                });
        }

        public void Update(Guid id, ProjectRequest project)
        {
            projectRepository.Update(new Project
            {
                Id = id,
                Name = project.Name,
                Description = project.Description,
                Creation = project.Creation,
                CustomerId = project.CustomerId,
                SupplierId = project.SupplierId
            });
        }

        public void Delete(Guid id)
        {
            projectRepository.Delete(id);
        }

        public ProjectResponse GetById(Guid id)
        {
            var project = projectRepository.GetById(id);

            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Creation = project.Creation,
                CustomerId = project.CustomerId,
                SupplierId = project.SupplierId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}