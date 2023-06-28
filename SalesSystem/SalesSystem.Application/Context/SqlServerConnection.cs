using SalesSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Application.Context
{
    public class SqlServerConnection 
    {
        private readonly string _connectionString;
        public SqlServerConnection(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection Connect()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
