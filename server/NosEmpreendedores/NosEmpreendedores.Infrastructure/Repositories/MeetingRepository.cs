using Dapper;
using NosEmpreendedores.Domain.Interfaces.Repositories;
using NosEmpreendedores.Domain.Models;
using NosEmpreendedores.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NosEmpreendedores.Infrastructure.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        protected readonly WeEntrepreneurContext Context;

        public MeetingRepository()
        {
            this.Context = new WeEntrepreneurContext();
        }

        public int Create(Meeting meeting)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", meeting.Id.ToByteArray());
            parameters.Add("Description", meeting.Description);
            parameters.Add("Date", meeting.Date);
            parameters.Add("Duration", meeting.Duration);
            parameters.Add("Local", meeting.Local);
            parameters.Add("Creation", DateTime.Now);
            parameters.Add("CustomerId", meeting.CustomerId.ToByteArray());
            parameters.Add("SupplierId", meeting.SupplierId.ToByteArray());

            string query = @"
                INSERT INTO WeEntrepreneurs.Meetings 
                    (Id, Date, Duration, Local, Description, Creation, CustomerId, SupplierId)
                VALUES
                    (?Id, ?Date, ?Duration, ?Local, ?Description, ?Creation, ?CustomerId, ?SupplierId)";

            return Context.Connection.Execute(query, parameters);
        }

        public IEnumerable<Meeting> Read()
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("LimitPerPage", PageConfiguration.LimitPerPage);

            string query = @"
                SELECT Id,
                       Description,
                       Date,
                       Duration,
                       Local,
                       Creation,
                       CustomerId,
                       SupplierId
                  FROM WeEntrepreneurs.Meetings
                 ORDER BY Description LIMIT ?LimitPerPage";

            var categories = Context
                .Connection
                .Query<dynamic>(query, parameters)
                .Select(meeting => new Meeting
                {
                    Id = new Guid(meeting.Id),
                    Date = meeting.Date,
                    Duration = meeting.Duration,
                    Local = meeting.Local,
                    Description = meeting.Description,
                    Creation = meeting.Creation,
                    CustomerId = new Guid(meeting.CustomerId),
                    SupplierId = new Guid(meeting.SupplierId)
                });

            return categories;
        }

        public void Update(Meeting meeting)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", meeting.Id.ToByteArray());

            List<string> clauses = new List<string>();

            if (!string.IsNullOrWhiteSpace(meeting.Description))
            {
                clauses.Add("Description = ?Description");
                parameters.Add("Description", meeting.Description);
            }
            if (meeting.Date != null)
            {
                clauses.Add("Date = ?Date");
                parameters.Add("Date", meeting.Date);
            }
            if (meeting.Duration != null)
            {
                clauses.Add("Duration = ?Duration");
                parameters.Add("Duration", meeting.Duration);
            }
            if (!string.IsNullOrWhiteSpace(meeting.Local))
            {
                clauses.Add("Local = ?Local");
                parameters.Add("Local", meeting.Local);
            }
            if (meeting.CustomerId != Guid.Empty)
            {
                clauses.Add("CustomerId = ?CustomerId");
                parameters.Add("CustomerId", meeting.CustomerId);
            }
            if (meeting.SupplierId != Guid.Empty)
            {
                clauses.Add("SupplierId = ?SupplierId");
                parameters.Add("SupplierId", meeting.SupplierId);
            }

            string query = $@"
                UPDATE WeEntrepreneurs.Meetings
                   SET {string.Join(", ", clauses)}
                 WHERE Id = ?Id";

            Context.Connection.Execute(query);
        }

        public void Delete(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = "DELETE FROM WeEntrepreneurs.Meetings WHERE Id = ?Id";

            Context.Connection.Execute(query, parameters);
        }

        public Meeting GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Id", id.ToByteArray());

            string query = $@"
                SELECT  Id,
                        Date,
                        Duration,
                        Local,
                        Description,
                        Creation,
                        CustomerId,
                        SupplierId
                  FROM WeEntrepreneurs.Meetings
                 WHERE Id = ?Id";

            var meeting = Context
                .Connection
                .QuerySingleOrDefault<dynamic>(query);

            return new Meeting
            {
                Id = new Guid(meeting.Id),
                Date = meeting.Date,
                Duration = meeting.Duration,
                Local = meeting.Local,
                Description = meeting.Description,
                Creation = meeting.Creation,
                CustomerId = new Guid(meeting.CustomerId),
                SupplierId = new Guid(meeting.SupplierId)
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}