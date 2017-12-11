using Dapper;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using CoelhoLigeiro.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Infrastructure.Repositories
{
    public class StepRepository : IStepRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public StepRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public int Create(Step step)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", step.Id.ToByteArray());
            parameters.Add("Name", step.Name);
            parameters.Add("Responsible", step.Responsible);
            parameters.Add("Begin", step.Begin);
            parameters.Add("End", step.End);
            parameters.Add("Description", step.Description);
            parameters.Add("Creation", DateTime.Now);
            parameters.Add("Situation", step.Situation);
            parameters.Add("ProjectId", step.ProjectId.ToByteArray());

            string query = @"
                INSERT INTO WeEntrepreneurs.Steps
                    (Id, Name, Responsible, Begin, End, Description, Creation, Situation, ProjectId)
                VALUES
                    (?Id, ?Name, ?Responsible, ?Begin, ?End, ?Description, ?Creation, ?Situation, ?ProjectId)";

            return Context.Connection.Execute(query, parameters);
        }

        public IEnumerable<Step> Read()
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = @"
                SELECT Id,
                       Name,
                       Description,
                       Responsible,
                       Begin,
                       End,
                       Creation,
                       Situation,
                       ProjectId
                  FROM WeEntrepreneurs.Steps
                 ORDER BY Name LIMIT ?LimitPerPage";

            var steps = Context
                .Connection
                .Query<dynamic>(query, parameters)
                .Select(step => new Step
                {
                    Id = new Guid(step.Id),
                    Name = step.Name,
                    Responsible = step.Responsible,
                    Begin = step.Begin,
                    End = step.End,
                    Description = step.Description,
                    Creation = step.Creation,
                    Situation = step.Situation,
                    ProjectId = new Guid(step.ProjectId)
                });

            return steps;
        }

        public void Update(Step step)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", step.Id.ToByteArray());

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(step.Name))
            {
                clauses.Add("Name = ?Name");
                parameters.Add("Name", step.Name);
            }
            if (!string.IsNullOrWhiteSpace(step.Name))
            {
                clauses.Add("Responsible = ?Responsible");
                parameters.Add("Responsible", step.Responsible);
            }
            if (step.Begin != null)
            {
                clauses.Add("Begin = ?Begin");
                parameters.Add("Begin", step.Begin);
            }
            if (step.End != null)
            {
                clauses.Add("End = ?End");
                parameters.Add("End", step.End);
            }
            if (!string.IsNullOrWhiteSpace(step.Description))
            {
                clauses.Add("Description = ?Description");
                parameters.Add("Description", step.Description);
            }
            if (step.Situation != 0)
            {
                clauses.Add("Situation = ?Situation");
                parameters.Add("Situation", step.Situation);
            }
            if (step.ProjectId != Guid.Empty)
            {
                clauses.Add("ProjectId = ?ProjectId");
                parameters.Add("ProjectId", step.ProjectId.ToByteArray());
            }

            string query = $@"
                UPDATE WeEntrepreneurs.Steps
                   SET {string.Join(", ", clauses)}
                 WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public void Delete(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = "DELETE FROM WeEntrepreneurs.Steps WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public Step GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = @"
                SELECT Id,
                       Name,
                       Responsible,
                       Begin,
                       End,
                       Description,
                       Creation,
                       Situation,
                       ProjectId
                  FROM WeEntrepreneurs.Steps
                 WHERE Id = ?Id";

            var step = Context
                .Connection
                .QuerySingleOrDefault<dynamic>(query, parameters);

            return new Step
            {
                Id = new Guid(step.Id),
                Name = step.Name,
                Responsible = step.Responsible,
                Begin = step.Begin,
                End = step.End,
                Description = step.Description,
                Creation = step.Creation,
                Situation = step.Situation,
                ProjectId = new Guid(step.ProjectId)
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}