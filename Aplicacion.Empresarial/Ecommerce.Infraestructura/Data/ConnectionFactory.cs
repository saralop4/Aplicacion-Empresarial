using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ecommerce.Infraestructura.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public IDbConnection GetConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();

                if (sqlConnection != null)
                {
                    sqlConnection.ConnectionString = _configuration.GetConnectionString("NorthwindConnection");
                    sqlConnection.Open();
                    return sqlConnection;
                }
                return null;
            }
        }
    }
}
