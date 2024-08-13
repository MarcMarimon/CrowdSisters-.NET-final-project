using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CrowdSisters.Conections
{
    public class Connection
    {
        private readonly string _connectionString;

        public Connection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
