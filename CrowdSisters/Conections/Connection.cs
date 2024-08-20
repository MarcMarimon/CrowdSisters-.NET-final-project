using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CrowdSisters.Conections
{
    public class Connection
    {
        private readonly string _connectionString;
        SqlConnection connection;
        public Connection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(_connectionString);

        }

        public SqlConnection GetSqlConn()
        {
            return connection;
        }
    }
}
