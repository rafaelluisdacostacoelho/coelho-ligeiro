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
    public class StepService : IStepService
    {
        private readonly IStepRepository stepRepository;

        public StepService(IStepRepository stepRepository)
        {
            this.stepRepository = stepRepository;
        }

        public int Create(StepRequest step)
        {
            return stepRepository.Create(new Step
            {
                Name = step.Name,
                Responsible = step.Responsible,
                Begin = step.Begin,
                End = step.End,
                Description = step.Description,
                Situation = step.Situation,
                ProjectId = step.ProjectId
            });
        }

        public IEnumerable<StepResponse> Read()
        {
            return stepRepository
                .Read()
                .Select(step => new StepResponse
                {
                    Id = step.Id,
                    Name = step.Name,
                    Responsible = step.Responsible,
                    Begin = step.Begin,
                    End = step.End,
                    Description = step.Description,
                    Creation = step.Creation,
                    Situation = step.Situation,
                    ProjectId = step.ProjectId
                });
        }

        public void Update(Guid id, StepRequest step)
        {
            stepRepository.Update(new Step
            {
                Id = id,
                Name = step.Name,
                Responsible = step.Responsible,
                Begin = step.Begin,
                End = step.End,
                Description = step.Description,
                Situation = step.Situation,
                ProjectId = step.ProjectId
            });
        }

        public void Delete(Guid id)
        {
            stepRepository.Delete(id);
        }

        public StepResponse GetById(Guid id)
        {
            var step = stepRepository.GetById(id);

            return new StepResponse
            {
                Id = step.Id,
                Name = step.Name,
                Responsible = step.Responsible,
                Begin = step.Begin,
                End = step.End,
                Description = step.Description,
                Creation = step.Creation,
                Situation = step.Situation,
                ProjectId = step.ProjectId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}