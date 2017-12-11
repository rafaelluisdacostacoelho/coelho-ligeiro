using Dapper;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using CoelhoLigeiro.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public AddressRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public int Create(Address address)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", address.Id.ToByteArray());
            parameters.Add("Description", address.Description);
            parameters.Add("State", address.State);
            parameters.Add("City", address.City);
            parameters.Add("District", address.District);
            parameters.Add("Number", address.Number);
            parameters.Add("SupplierId", address.SupplierId.ToByteArray());

            string query = @"
                INSERT INTO WeEntrepreneurs.Addresses 
                    (Id, Description, State, City, District, Number, SupplierId)
                VALUES
                    (?Id, ?Description, ?State, ?City, ?District, ?Number, ?SupplierId)";

            return Context.Connection.Execute(query, parameters);
        }

        public IEnumerable<Address> Read()
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = @"
                SELECT Id,
                       Description,
                       State,
                       City,
                       District,
                       Number,
                       SupplierId
                  FROM WeEntrepreneurs.Addresses
                 ORDER BY Description LIMIT ?LimitPerPage";

            var addresses = Context
                .Connection
                .Query<dynamic>(query, parameters)
                .Select(address => new Address
                {
                    Id = new Guid(address.Id),
                    Description = address.Description,
                    State = address.State,
                    City = address.City,
                    District = address.District,
                    Number = address.Number,
                    SupplierId = new Guid(address.SupplierId)
                });

            return addresses;
        }

        public void Update(Address address)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("Id", address.Id.ToByteArray());

                List<string> clauses = new List<string>();

                if (!string.IsNullOrWhiteSpace(address.Description))
                {
                    clauses.Add("Description = ?Description");
                    parameters.Add("Description", address.Description);
                }
                if (!string.IsNullOrWhiteSpace(address.State))
                {
                    clauses.Add("State = ?State");
                    parameters.Add("State", address.State);
                }
                if (!string.IsNullOrWhiteSpace(address.City))
                {
                    clauses.Add("City = ?City");
                    parameters.Add("City", address.City);
                }
                if (!string.IsNullOrWhiteSpace(address.District))
                {
                    clauses.Add("District = ?District");
                    parameters.Add("District", address.District);
                }
                if (!string.IsNullOrWhiteSpace(address.Number))
                {
                    clauses.Add("Number = ?Number");
                    parameters.Add("Number", address.Number);
                }
                if (address.SupplierId != Guid.Empty)
                {
                    clauses.Add("SupplierId = ?SupplierId");
                    parameters.Add("SupplierId", address.SupplierId.ToByteArray());
                }

                string query = $@"
                    UPDATE WeEntrepreneurs.Addresses
                       SET {string.Join(", ", clauses)}
                     WHERE Id = ?Id";

                Context.Connection.Execute(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = "DELETE FROM WeEntrepreneurs.Addresses WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public Address GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = $@"
                SELECT Id,
                       Description,
                       State,
                       City,
                       District,
                       Number,
                       SupplierId
                  FROM WeEntrepreneurs.Addresses
                 WHERE Id = ?Id";

            var address = Context
                .Connection
                .QuerySingleOrDefault<dynamic>(query, parameters);

            return new Address
            {
                Id = new Guid(address.Id),
                Description = address.Description,
                State = address.State,
                City = address.City,
                District = address.District,
                Number = address.Number,
                SupplierId = new Guid(address.SupplierId)
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}