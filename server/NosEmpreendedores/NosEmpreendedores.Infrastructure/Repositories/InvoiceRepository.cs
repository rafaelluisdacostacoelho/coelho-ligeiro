using Dapper;
using NosEmpreendedores.Domain.Interfaces.Repositories;
using NosEmpreendedores.Domain.Models;
using NosEmpreendedores.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NosEmpreendedores.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public InvoiceRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public int Create(Invoice invoice)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", invoice.Id.ToByteArray());
            parameters.Add("Name", invoice.Name);
            parameters.Add("Value", invoice.Value);
            parameters.Add("Recurrence", invoice.Recurrence);
            parameters.Add("Date", invoice.Date);
            parameters.Add("Description", invoice.Description);
            parameters.Add("Creation", invoice.Creation);
            parameters.Add("Situation", invoice.Situation);
            parameters.Add("CustomerId", invoice.CustomerId);
            parameters.Add("SupplierId", invoice.SupplierId);

            string query = @"
                INSERT INTO WeEntrepreneurs.Invoices
                    (Id, Name, Value, Recurrence, Date, Description, Creation, Situation, CustomerId, SupplierId)
                VALUES
                    (?Id, ?Name, ?Value, ?Recurrence, ?Description, ?Creation, ?Situation, ?CustomerId, ?SupplierId)";

            return Context.Connection.Execute(query, parameters);
        }

        public IEnumerable<Invoice> Read()
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = $@"
                SELECT Id,
                       Name,
                       Value,
                       Recurrence,
                       Date,
                       Description,
                       Creation,
                       Situation,
                       CustomerId,
                       SupplierId
                  FROM WeEntrepreneurs.Invoices
                 ORDER BY Name LIMIT ?LimitPerPage";

            var categories = Context
                .Connection
                .Query<dynamic>(query, parameters)
                .Select(invoice => new Invoice
                {
                    Id = new Guid(invoice.Id),
                    Name = invoice.Name,
                    Value = invoice.Value,
                    Recurrence = invoice.Recurrence,
                    Date = invoice.Date,
                    Description = invoice.Description,
                    Creation = invoice.Creation,
                    Situation = invoice.Situation,
                    CustomerId = new Guid(invoice.CustomerId),
                    SupplierId = new Guid(invoice.SupplierId)
                });

            return categories;
        }

        public void Update(Invoice invoice)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", invoice.Id.ToByteArray());

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(invoice.Name))
            {
                clauses.Add("Name = ?Name");
                parameters.Add("Name", invoice.Name);
            }
            if (invoice.Value != 0)
            {
                clauses.Add("Value = ?Value");
                parameters.Add("Value", invoice.Value);
            }
            if (invoice.Recurrence != 0)
            {
                clauses.Add("Recurrence = ?Recurrence");
                parameters.Add("Recurrence", invoice.Recurrence);
            }
            if (invoice.Date != null)
            {
                clauses.Add("Date = ?Date");
                parameters.Add("Date", invoice.Date);
            }
            if (!string.IsNullOrWhiteSpace(invoice.Description))
            {
                clauses.Add("Description = ?Description");
                parameters.Add("Description", invoice.Description);
            }
            if (invoice.Situation != 0)
            {
                clauses.Add("Situation = ?Situation");
                parameters.Add("Situation", invoice.Situation);
            }
            if (invoice.CustomerId != Guid.Empty)
            {
                clauses.Add("CustomerId = ?CustomerId");
                parameters.Add("CustomerId", invoice.CustomerId);
            }
            if (invoice.SupplierId != Guid.Empty)
            {
                clauses.Add("SupplierId = ?SupplierId");
                parameters.Add("SupplierId", invoice.SupplierId);
            }

            string query = $@"
                UPDATE WeEntrepreneurs.Invoices
                   SET {string.Join(", ", clauses)}
                 WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public void Delete(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = "DELETE FROM WeEntrepreneurs.Invoices WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public Invoice GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = @"
                SELECT Id,
                       Name,
                       Value,
                       Recurrence,
                       Date,
                       Description,
                       Creation,
                       Situation,
                       CustomerId,
                       SupplierId
                  FROM WeEntrepreneurs.Invoices
                 WHERE Id = ?Id";

            var invoice = Context
                .Connection
                .QuerySingleOrDefault<dynamic>(query, parameters);

            return new Invoice
            {
                Id = new Guid(invoice.Id),
                Name = invoice.Name,
                Value = invoice.Value,
                Recurrence = invoice.Recurrence,
                Date = invoice.Date,
                Description = invoice.Description,
                Creation = invoice.Creation,
                Situation = invoice.Situation,
                CustomerId = new Guid(invoice.CustomerId),
                SupplierId = new Guid(invoice.SupplierId)
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}