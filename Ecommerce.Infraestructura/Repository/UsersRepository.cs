using Dapper;
using Ecommerce.Dominio.Entity;
using Ecommerce.Infraestructura.Interfaces;
using Ecommerce.Transversal.Interfaces;
using System.Data;

namespace Ecommerce.Infraestructura.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public UsersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public Users Authenticate(string userName, string password)
        {
            using(var connection = _connectionFactory.GetConnection)
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName);
                parameters.Add("Password", password);  

                var user = connection.QuerySingle<Users>(query,param: parameters, commandType: CommandType.StoredProcedure);
                //con QuerySingle le decimos que nos devuelva del procedimiento un solo registro

                 
                return user;

            }
        }
    }
}
