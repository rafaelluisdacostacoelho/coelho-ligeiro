using Dapper;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using CoelhoLigeiro.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public EventRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public int Create(Event entity)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", entity.Id.ToByteArray());
            parameters.Add("Name", entity.Name);
            parameters.Add("Local", entity.Local);
            parameters.Add("Begin", entity.Begin);
            parameters.Add("End", entity.End);
            parameters.Add("SupplierId", entity.SupplierId.ToByteArray());

            string query = @"
                INSERT INTO WeEntrepreneurs.Events
                    (Id, Name, Local, Begin, End, SupplierId)
                VALUES
                    (?Id, ?Name, ?Local, ?Begin, ?End, ?SupplierId)";

            return Context.Connection.Execute(query, parameters);
        }

        public IEnumerable<Event> Read()
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = @"
                SELECT Id,
                       Name,
                       Local,
                       Begin,
                       End,
                       SupplierId
                  FROM WeEntrepreneurs.Events
                 ORDER BY Name LIMIT ?LimitPerPage";

            var categories = Context
                .Connection
                .Query<dynamic>(query, parameters)
                .Select(entity => new Event
                {
                    Id = new Guid(entity.Id),
                    Name = entity.Name,
                    Local = entity.Local,
                    Begin = entity.Begin,
                    End = entity.End,
                    SupplierId = new Guid(entity.SupplierId)
                });

            return categories;
        }

        public void Update(Event entity)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", entity.Id.ToByteArray());

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                clauses.Add("Name = ?Name");
                parameters.Add("Name", entity.Name);
            }
            if (!string.IsNullOrWhiteSpace(entity.Local))
            {
                clauses.Add("Local = ?Local");
                parameters.Add("Local", entity.Local);
            }
            if (entity.Begin != null)
            {
                clauses.Add("Begin = ?Begin");
                parameters.Add("Begin", entity.Begin);
            }
            if (entity.End != null)
            {
                clauses.Add("End = ?End");
                parameters.Add("End", entity.End);
            }
            if (entity.SupplierId != Guid.Empty)
            {
                clauses.Add("SupplierId = ?SupplierId");
                parameters.Add("SupplierId", entity.SupplierId.ToByteArray());
            }

            string query = $@"
                UPDATE WeEntrepreneurs.Events
                   SET {string.Join(", ", clauses)}
                 WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public void Delete(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = "DELETE FROM WeEntrepreneurs.Events WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public Event GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = @"
                select Id,
                       Name,
                       Local,
                       Begin,
                       End,
                       SupplierId
                  from WeEntrepreneurs.Events
                 where Id = ?Id";

            var entity = Context
                .Connection
                .QuerySingleOrDefault<dynamic>(query, parameters);

            return new Event
            {
                Id = new Guid(entity.Id),
                Name = entity.Name,
                Local = entity.Local,
                Begin = entity.Begin,
                End = entity.End,
                SupplierId = new Guid(entity.SupplierId)
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}