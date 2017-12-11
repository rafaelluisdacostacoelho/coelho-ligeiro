using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CoelhoLigeiro.Infrastructure.Contexts
{
    public class WeEntrepreneurContext : IDisposable
    {
        private readonly string server = "127.0.0.1";
        private readonly string database = "WeEntrepreneurs";
        private readonly string password = "r3b3l1on";
        private readonly string user = "root";
        private readonly string port = "3306";
        public IDbConnection Connection { get; }

        public WeEntrepreneurContext()
        {
            try
            {
                Connection = new MySqlConnection($"Server={server};Database={database};User ID={user};Password={password};Port={port};");
                Connection.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    Connection.Dispose();
                }

                disposed = true;
            }
        }

        ~WeEntrepreneurContext()
        {
            Dispose(false);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
    }
}