using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ecommerce.Infraestructura.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration; // Esta interfaz permite acceder a las propiedades de configuración de la solución.
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("NorthwindConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
