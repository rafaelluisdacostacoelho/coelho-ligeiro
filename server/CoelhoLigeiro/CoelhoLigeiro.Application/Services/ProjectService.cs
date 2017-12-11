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