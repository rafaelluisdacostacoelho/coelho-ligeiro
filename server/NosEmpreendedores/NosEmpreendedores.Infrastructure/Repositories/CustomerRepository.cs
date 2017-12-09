using Dapper;
using NosEmpreendedores.Domain.Interfaces.Repositories;
using NosEmpreendedores.Domain.Models;
using NosEmpreendedores.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosEmpreendedores.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public CustomerRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public async Task CreateAsync(Customer customer)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", customer.Id.ToByteArray());
            parameters.Add("Name", customer.Name);
            parameters.Add("Email", customer.Email);
            parameters.Add("Responsible", customer.Responsible);
            parameters.Add("Description", customer.Description);
            parameters.Add("Creation", customer.Creation);
            parameters.Add("SupplierId", customer.SupplierId.ToByteArray());

            string query = @"
                INSERT INTO WeEntrepreneurs.Customers
                    (Id, Name, Email, Responsible, Description, Creation, SupplierId)
                VALUES
                    (?Id, ?Name, ?Email, ?Responsible, ?Description, ?Creation, ?SupplierId)";

            await Context.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<Customer>> ReadAsync(CustomerFilter filter)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("PersonType", (int)filter.PersonType);
            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                clauses.Add($"Name LIKE ?Description");
                parameters.Add("Description", $"%{filter.Description}%");
            }
            if (filter.Begin != null)
            {
                clauses.Add("?Begin <= Creation");
                parameters.Add("Begin", filter.Begin);
            }
            if (filter.End != null)
            {
                clauses.Add("Creation <= ?End");
                parameters.Add("End", filter.End);
            }

            string query = $@"
                SELECT Id,
                       Name,
                       Email,
                       Responsible,
                       Description,
                       Creation,
                       SupplierId,
                       PersonType
                  FROM WeEntrepreneurs.Customers
                 WHERE PersonType = ?PersonType 
                   AND ${string.Join(" AND ", clauses)}
                 ORDER BY Name LIMIT ?LimitPerPage";

            var customers = await Context.Connection.QueryAsync<dynamic>(query, parameters);

            return customers.Select(customer => new Customer
            {
                Id = new Guid(customer.Id),
                Name = customer.Name,
                Email = customer.Email,
                Responsible = customer.Responsible,
                Description = customer.Description,
                SupplierId = new Guid(customer.SupplierId),
                PersonType = customer.PersonType
            });
        }

        public async Task UpdateAsync(Customer customer)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", customer.Id.ToByteArray());
            parameters.Add("PersonType", customer.PersonType);

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(customer.Name))
            {
                clauses.Add("Name = ?Name");
                parameters.Add("Name", customer.Name);
            }
            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                clauses.Add("Email = ?Email");
                parameters.Add("Email", customer.Email);
            }
            if (!string.IsNullOrWhiteSpace(customer.Responsible))
            {
                clauses.Add("Responsible = ?Responsible");
                parameters.Add("Responsible", customer.Responsible);
            }
            if (!string.IsNullOrWhiteSpace(customer.Description))
            {
                clauses.Add("Description = ?Description");
                parameters.Add("Description", customer.Description);
            }
            if (customer.SupplierId != Guid.Empty)
            {
                clauses.Add("SupplierId = ?SupplierId");
                parameters.Add("SupplierId", customer.SupplierId);
            }

            string query = $@"
                UPDATE WeEntrepreneurs.Customers
                   SET PersonType = ?PersonType,
                       {string.Join(", ", clauses)}
                 WHERE Id = ?Id";

            await Context.Connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteAsync(Guid[] guids)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Guids", guids.Select(guid => guid.ToByteArray()));

            string query = $@"DELETE FROM WeEntrepreneurs.Customers WHERE Id = ?Guids";

            await Context.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<Customer> GetByIdAsync(Guid guid)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Guid", guid.ToByteArray());

            string query = $@"
                SELECT Id,
                       Name,
                       Email,
                       Responsible,
                       Description,
                       Creation,
                       SupplierId,
                       PersonType
                  FROM WeEntrepreneurs.Customers
                 WHERE Id = ?Guid";

            var customer = await Context.Connection.QuerySingleOrDefaultAsync<dynamic>(query, parameters);

            return new Customer
            {
                Id = new Guid(customer.Id),
                Name = customer.Name,
                Email = customer.Email,
                Responsible = customer.Responsible,
                Description = customer.Description,
                SupplierId = new Guid(customer.SupplierId),
                PersonType = customer.PersonType
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}