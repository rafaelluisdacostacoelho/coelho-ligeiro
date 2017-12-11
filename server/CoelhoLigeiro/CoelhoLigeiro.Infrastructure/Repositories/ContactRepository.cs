using Dapper;
using CoelhoLigeiro.Domain.Enumerators;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using CoelhoLigeiro.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public ContactRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public int Create(Contact category)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", category.Id.ToByteArray());
            parameters.Add("Description", category.Description);
            parameters.Add("Creation", DateTime.Now);
            parameters.Add("CustomerId", category.CustomerId);
            parameters.Add("SupplierId", category.SupplierId.ToByteArray());

            string query = @"
                INSERT INTO WeEntrepreneurs.Contacts 
                    (Id, Description, Creation, CustomerId, SupplierId)
                VALUES
                    (?Id, ?Description, ?Creation, ?CustomerId, ?SupplierId)";

            return Context.Connection.Execute(query);
        }

        public IEnumerable<Contact> Read()
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = @"
                SELECT Id,
                       Description,
                       Creation,
                       CustomerId,
                       SupplierId
                  FROM WeEntrepreneurs.Contacts
                 ORDER BY Description LIMIT ?LimitPerPage";

            var categories = Context
                .Connection
                .Query<dynamic>(query, parameters)
                .Select(category => new Contact
                {
                    Id = new Guid(category.Id),
                    Description = category.Description,
                    Creation = category.Creation,
                    CustomerId = new Guid(category.CustomerId),
                    SupplierId = new Guid(category.SupplierId)
                });

            return categories;
        }

        public void Update(Contact category)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", category.Id.ToByteArray());

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(category.Description))
            {
                clauses.Add("Description = ?Description");
                parameters.Add("Description", category.Description);
            }
            if (category.CustomerId != Guid.Empty)
            {
                clauses.Add("CustomerId = ?CustomerId");
                parameters.Add("CustomerId", category.CustomerId.ToByteArray());
            }
            if (category.SupplierId != Guid.Empty)
            {
                clauses.Add("SupplierId = ?SupplierId'");
                parameters.Add("SupplierId", category.SupplierId.ToByteArray());
            }

            string query = $@"
                UPDATE WeEntrepreneurs.Contacts
                   SET {string.Join(", ", clauses)}
                 WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public void Delete(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = "DELETE FROM WeEntrepreneurs.Contacts WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public Contact GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = @"
                SELECT Id,
                       Description,
                       Creation,
                       CustomerId,
                       SupplierId
                  FROM WeEntrepreneurs.Contacts
                 WHERE Id = ?Id";

            var category = Context
                .Connection
                .QuerySingleOrDefault<dynamic>(query, parameters);

            return new Contact
            {
                Id = new Guid(category.Id),
                Description = category.Description,
                Creation = category.Creation,
                CustomerId = new Guid(category.CustomerId),
                SupplierId = new Guid(category.SupplierId)
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}