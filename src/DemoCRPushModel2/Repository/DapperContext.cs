using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DemoCRPushModel2.Repository
{
    public interface IDapperContext : IDisposable
    {
        IDbConnection db { get; }
    }

    public class DapperContext : IDapperContext
    {
        private string _providerName;
        private string _connectionString;
        private IDbConnection _db;

        public DapperContext()
        {
            var dbName = System.IO.Directory.GetCurrentDirectory() + "\\Northwind.db";

            _providerName = "System.Data.SQLite";
            _connectionString = "Data Source=" + dbName;
        }

        public IDbConnection db
        {
            get
            {
                if (_db == null)
                {
                    try
                    {
                        _db = GetOpenConnection(_providerName, _connectionString);
                    }
                    catch
                    {
                    }
                }

                return _db;
            }
        }

        private IDbConnection GetOpenConnection(string providerName, string connectionString)
        {
            DbConnection conn = null;

            try
            {
                DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);
                conn = provider.CreateConnection();
                conn.ConnectionString = connectionString;
                conn.Open();

            }
            catch (Exception)
            {
            }

            return conn;
        }

        public void Dispose()
        {
            if (_db != null)
            {
                try
                {
                    if (_db.State != ConnectionState.Closed)
                        _db.Close();
                }
                finally
                {
                    _db.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
