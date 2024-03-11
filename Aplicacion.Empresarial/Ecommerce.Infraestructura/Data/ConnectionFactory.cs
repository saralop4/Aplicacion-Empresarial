using AutoMapper.Configuration;
using MicrosoftConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Ecommerce.Transversal.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infraestructura.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly MicrosoftConfiguration _configuration;

        public ConnectionFactory(MicrosoftConfiguration configuration)
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
