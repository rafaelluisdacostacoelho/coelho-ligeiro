using Dapper;
using MySql.Data.MySqlClient;
using NosEmpreendedores.Domain.Enumerators;
using NosEmpreendedores.Domain.Interfaces.Repositories;
using NosEmpreendedores.Domain.Models;
using NosEmpreendedores.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NosEmpreendedores.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public CategoryRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public async Task CreateAsync(Category category)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", category.Id.ToByteArray());
            parameters.Add("Name", category.Name);

            string query = "INSERT INTO WeEntrepreneurs.Categories (Id, Name) VALUES (?Id, ?Name)";

            await Context.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<Category>> ReadAsync(string description)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = @"
                SELECT Id, Name
                  FROM WeEntrepreneurs.Categories
                  {0}
                 ORDER BY Name LIMIT ?LimitPerPage";

            if (!string.IsNullOrWhiteSpace(description))
            {
                parameters.Add("Description", $"%{description}%");

                query = string.Format(query, $"WHERE Name LIKE ?Description");
            }
            else
            {
                query = string.Format(query, string.Empty);
            }

            var categories = await Context.Connection.QueryAsync<dynamic>(query, parameters);

            return categories
                .Select(category => new Category
                {
                    Id = new Guid(category.Id),
                    Name = category.Name
                }); ;
        }

        public async Task UpdateAsync(Category category)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Name", category.Name);
            parameters.Add("Id", category.Id.ToByteArray());

            string query = @"
                UPDATE WeEntrepreneurs.Categories
                   SET Name = ?Name
                 WHERE Id = ?Id";

            await Context.Connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteAsync(Guid[] guids)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Guids", guids.Select(id => id.ToByteArray()));

            string query = "DELETE FROM WeEntrepreneurs.Categories WHERE Id IN ?Guids";

            await Context.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            const string query = @"
                SELECT Id, Name
                  FROM WeEntrepreneurs.Categories
                 WHERE Id = ?Id";

            var category = await Context.Connection.QuerySingleOrDefaultAsync<dynamic>(query, parameters);

            return new Category
            {
                Id = new Guid(category.Id),
                Name = category.Name
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}