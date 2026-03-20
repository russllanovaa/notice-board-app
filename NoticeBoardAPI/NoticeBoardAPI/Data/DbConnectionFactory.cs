using Microsoft.Data.SqlClient;
using NoticeBoardAPI.Interfaces;
using System.Data;

namespace NoticeBoardAPI.Data
{
    public class DbConnectionFactory: IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}