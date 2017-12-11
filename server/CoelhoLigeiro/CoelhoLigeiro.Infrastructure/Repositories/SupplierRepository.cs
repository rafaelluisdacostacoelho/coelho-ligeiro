using Dapper;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using CoelhoLigeiro.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoelhoLigeiro.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public SupplierRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public async Task CreateAsync(Supplier supplier)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", supplier.Id.ToByteArray());
            parameters.Add("Name", supplier.Name);
            parameters.Add("TradingName", supplier.TradingName);
            parameters.Add("CorporateTaxpayerRegistry", supplier.CorporateTaxpayerRegistry);
            parameters.Add("Email", supplier.Email);
            parameters.Add("Situation", supplier.Situation);
            parameters.Add("Photo", supplier.Photo);
            parameters.Add("Creation", supplier.Creation);
            parameters.Add("CategoryId", supplier.CategoryId.ToByteArray());

            string query = @"
                INSERT INTO WeEntrepreneurs.Suppliers
                    (Id, Name, TradingName, CorporateTaxpayerRegistry, Email, Situation, Photo, Creation, CategoryId)
                VALUES
                    (?Id, ?Name, ?TradingName, ?CorporateTaxpayerRegistry, ?Email, ?Situation, ?Photo, ?Creation, ?CategoryId )";

            await Context.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<Supplier>> ReadAsync(SupplierFilter filter)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = @"
                SELECT Id, 
                       Name,
                       TradingName,
                       CorporateTaxpayerRegistry,
                       Email,
                       Situation,
                       Photo,
                       Creation,
                       CategoryId
                  FROM WeEntrepreneurs.Suppliers
                 ORDER BY Name LIMIT ?LimitPerPage";

            var suppliers = await Context.Connection.QueryAsync<dynamic>(query, parameters);

            return suppliers.Select(supplier => new Supplier
            {
                Id = new Guid(supplier.Id),
                Name = supplier.Name,
                TradingName = supplier.TradingName,
                CorporateTaxpayerRegistry = supplier.CorporateTaxpayerRegistry,
                Email = supplier.Email,
                Situation = supplier.Situation,
                Photo = supplier.Photo,
                Creation = supplier.Creation,
                CategoryId = new Guid(supplier.CategoryId)
            });
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", supplier.Id.ToByteArray());

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(supplier.Name))
            {
                clauses.Add("Name = ?Name");
                parameters.Add("Name", supplier.Name);
            }
            if (!string.IsNullOrWhiteSpace(supplier.TradingName))
            {
                clauses.Add("TradingName = TradingName");
                parameters.Add("TradingName", supplier.TradingName);
            }
            if (!string.IsNullOrWhiteSpace(supplier.CorporateTaxpayerRegistry))
            {
                clauses.Add("CorporateTaxpayerRegistry = ?CorporateTaxpayerRegistry");
                parameters.Add("CorporateTaxpayerRegistry", supplier.CorporateTaxpayerRegistry);
            }
            if (!string.IsNullOrWhiteSpace(supplier.Email))
            {
                clauses.Add("Email = ?Email");
                parameters.Add("Email", supplier.Email);
            }
            if (supplier.Photo != null)
            {
                clauses.Add("Photo = ?Photo");
                parameters.Add("Photo", supplier.Photo);
            }
            if (supplier.CategoryId != Guid.Empty)
            {
                clauses.Add("CategoryId = ?CategoryId");
                parameters.Add("CategoryId", supplier.CategoryId.ToByteArray());
            }

            string query = $@"
                UPDATE WeEntrepreneurs.Suppliers
                   SET {string.Join(", ", clauses)}
                 WHERE Id = ?Id";

            await Context.Connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteAsync(Guid[] guids)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Guids", guids.Select(guid => guid.ToByteArray()));

            string query = "DELETE FROM WeEntrepreneurs.Suppliers WHERE Id = ?Guids";

            await Context.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<Supplier> GetByIdAsync(Guid guid)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Guid", guid.ToByteArray());

            string query = @"
                SELECT Id, 
                       Name,
                       TradingName,
                       CorporateTaxpayerRegistry,
                       Email,
                       Situation,
                       Photo,
                       Creation,
                       CategoryId
                  FROM WeEntrepreneurs.Suppliers
                 WHERE Id = ?Guid";

            var supplier = await Context.Connection.QuerySingleOrDefaultAsync<dynamic>(query, parameters);

            return new Supplier
            {
                Id = new Guid(supplier.Id),
                Name = supplier.Name,
                TradingName = supplier.TradingName,
                CorporateTaxpayerRegistry = supplier.CorporateTaxpayerRegistry,
                Email = supplier.Email,
                Situation = supplier.Situation,
                Photo = supplier.Photo,
                Creation = supplier.Creation,
                CategoryId = new Guid(supplier.CategoryId)
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}