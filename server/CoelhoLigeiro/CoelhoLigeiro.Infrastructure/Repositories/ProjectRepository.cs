using Dapper;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using CoelhoLigeiro.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public ProjectRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public int Create(Project project)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", project.Id.ToByteArray());
            parameters.Add("Name", project.Name);
            parameters.Add("Description", project.Description);
            parameters.Add("Creation", DateTime.Now);
            parameters.Add("CustomerId", project.CustomerId.ToByteArray());
            parameters.Add("SupplierId", project.SupplierId.ToByteArray());

            string query = @"
                INSERT INTO WeEntrepreneurs.Projects
                    (Id, Name, Description, Creation, CustomerId, SupplierId)
                VALUES
                    (?Id, ?Name, ?Description, ?Creation, ?CustomerId, ?SupplierId)";

            return Context.Connection.Execute(query, parameters);
        }

        public IEnumerable<Project> Read()
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = @"
                SELECT Id, Name, Description, Creation, CustomerId, SupplierId
                  FROM WeEntrepreneurs.Projects
                 ORDER BY Name LIMIT ?LimitPerPage";

            var categories = Context
                .Connection
                .Query<dynamic>(query, parameters)
                .Select(project => new Project
                {
                    Id = new Guid(project.Id),
                    Name = project.Name,
                    Description = project.Description,
                    Creation = project.Creation,
                    CustomerId = new Guid(project.CustomerId),
                    SupplierId = new Guid(project.SupplierId)
                });

            return categories;
        }

        public void Update(Project project)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", project.Id.ToByteArray());

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(project.Name))
            {
                clauses.Add("Name = ?Name");
                parameters.Add("Name", project.Name);
            }
            if (!string.IsNullOrWhiteSpace(project.Description))
            {
                clauses.Add("Description = ?Description");
                parameters.Add("Description", project.Description);
            }
            if (project.Creation != null)
            {
                clauses.Add("Creation = ?Creation");
                parameters.Add("Creation", DateTime.Now);
            }
            if (project.CustomerId != Guid.Empty)
            {
                clauses.Add("CustomerId = ?CustomerId");
                parameters.Add("CustomerId", project.CustomerId.ToByteArray());
            }
            if (project.SupplierId != Guid.Empty)
            {
                clauses.Add("SupplierId = ?SupplierId");
                parameters.Add("SupplierId", project.SupplierId.ToByteArray());
            }

            string query = $@"
                UPDATE WeEntrepreneurs.Projects
                   SET {string.Join(", ", clauses)}
                 WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public void Delete(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = "DELETE FROM WeEntrepreneurs.Projects WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public Project GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = $@"
                SELECT Id,
                       Name,
                       Description,
                       Creation,
                       CustomerId, 
                       SupplierId
                  FROM WeEntrepreneurs.Projects
                 WHERE Id = ?Id";

            var project = Context
                .Connection
                .QuerySingleOrDefault<dynamic>(query, parameters);

            return new Project
            {
                Id = new Guid(project.Id),
                Name = project.Name,
                Description = project.Description,
                Creation = project.Creation,
                CustomerId = new Guid(project.CustomerId),
                SupplierId = new Guid(project.SupplierId)
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}