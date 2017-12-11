using Dapper;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using CoelhoLigeiro.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public UserRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public int Create(User user)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", user.Id.ToByteArray());
            parameters.Add("Name", user.Name);
            parameters.Add("Email", user.Email);
            parameters.Add("Password", user.Password);
            parameters.Add("Creation", DateTime.Now);
            parameters.Add("Situation", user.Situation);
            parameters.Add("SupplierId", user.SupplierId.ToByteArray());

            string query = @"
                INSERT INTO WeEntrepreneurs.Categories
                    (Id, Name, Email, Password, Creation, Situation, SupplierId)
                VALUES
                    (?Id, ?Name, ?Email, ?Password, ?Creation, ?Situation, ?SupplierId)";

            return Context.Connection.Execute(query, parameters);
        }

        public IEnumerable<User> Read()
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = @"
                SELECT Id,
                       Name,
                       Email,
                       Password,
                       Creation,
                       Situation,
                       SupplierId
                  FROM WeEntrepreneurs.Categories
                 ORDER BY Name LIMIT ?LimitPerPage";

            var categories = Context
                .Connection
                .Query<dynamic>(query, parameters)
                .Select(user => new User
                {
                    Id = new Guid(user.Id),
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Creation = user.Creation,
                    Situation = user.Situation,
                    SupplierId = new Guid(user.SupplierId)
                });

            return categories;
        }

        public void Update(User user)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", user.Id.ToByteArray());

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                clauses.Add("Name = ?Name");
                parameters.Add("Name", user.Name);
            }
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                clauses.Add("Email = ?Email");
                parameters.Add("Email", user.Email);
            }
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                clauses.Add("Password = ?Password");
                parameters.Add("Password", user.Password);
            }
            if (user.Situation != 0)
            {
                clauses.Add("Situation = ?Situation");
                parameters.Add("Situation", user.Situation);
            }
            if (user.SupplierId != Guid.Empty)
            {
                clauses.Add("SupplierId = ?SupplierId");
                parameters.Add("SupplierId", user.SupplierId);
            }

            string query = $@"
                UPDATE WeEntrepreneurs.Customer
                   SET {string.Join(", ", clauses)}
                 WHERE Id = ?Id";

            Context.Connection.Execute(query);
        }

        public void Delete(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = "DELETE FROM WeEntrepreneurs.Categories WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public User GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = @"
                SELECT Id,
                       Name,
                       Email,
                       Password,
                       Creation,
                       Situation,
                       SupplierId
                  FROM WeEntrepreneurs.Users
                 WHERE Id = ?Id";

            var user = Context
                .Connection
                .QuerySingleOrDefault<dynamic>(query, parameters);

            return new User
            {
                Id = new Guid(user.Id),
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Creation = user.Creation,
                Situation = user.Situation,
                SupplierId = new Guid(user.SupplierId)
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}