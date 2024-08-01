using Dapper;
using Ecommerce.Dominio.Entity;
using Ecommerce.Infraestructura.Data;
using Ecommerce.Infraestructura.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructura.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DapperContext _context;
        public CustomersRepository(DapperContext context)
        {
            _context = context;

        }

        #region Metodos Sincronos
        public bool Delete(string customerId)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersDelete"; //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); //para el uso de dapper se recomienda el uso de parametros dinamicos
                                                          //parameters es una lista de parametros
                parameters.Add("CustomerID", customerId);


                var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return result > 0;
            }

        }
        public Customers Get(string customerId)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersGetByID"; //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); //para el uso de dapper se recomienda el uso de parametros dinamicos
                                                          //parameters es una lista de parametros
                parameters.Add("CustomerID", customerId);


                var customer = connection.QuerySingle<Customers>(query, param: parameters, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return customer;
            }
        }

        public IEnumerable<Customers> GetAll()
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersList"; //nombre del procedimiento almacenado
                var customers = connection.Query<Customers>(query, param: null, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return customers;
            }
        }
        public bool Insert(Customers customer)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersInsert"; //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); //para el uso de dapper se recomienda el uso de parametros dinamicos
                                                          //parameters es una lista de parametros
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return result > 0;

            }
        }

        public bool Update(Customers customer)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersUpdate"; //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); //para el uso de dapper se recomienda el uso de parametros dinamicos
                                                          //parameters es una lista de parametros
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return result > 0;
            }
        }
        public IEnumerable<Customers> GetAllWithPagination(int pageNumber, int pageSize)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {
                var query = "CustomersListWithPagination";
                var parameters = new DynamicParameters();

                parameters.Add("PageNumbe", pageNumber);
                parameters.Add("PageSize", pageSize);

                var result = connection.Query<Customers>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int Count()
        {
          using var connection = _context.CreateConnection();
             var query = "select Count(*) from Customers";
            var parameters = new DynamicParameters();   

            var result = connection.ExecuteScalar<int>(query,commandType: CommandType.Text);// con el metodo ExecuteScalar ejecutamos la instruccion para saber cuantos registros tiene la tabla
            return result;
        }

        #endregion Fin Síncronos


        #region Metodos Asíncronos
        public async Task<bool> DeleteAsync(string customerId)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersDelete"; //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); //para el uso de dapper se recomienda el uso de parametros dinamicos
                                                          //parameters es una lista de parametros
                parameters.Add("CustomerID", customerId);


                var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return result > 0;
            }
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersList"; //nombre del procedimiento almacenado

                var customers = await connection.QueryAsync<Customers>(query, param: null, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return customers;
            }
        }

        public async Task<Customers> GetAsync(string customerId)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersGetByID"; //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); //para el uso de dapper se recomienda el uso de parametros dinamicos
                                                          //parameters es una lista de parametros
                parameters.Add("CustomerID", customerId);


                var customer = await connection.QuerySingleAsync<Customers>(query, param: parameters, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return customer;
            }
        }

        public async Task<bool> InsertAsync(Customers customer)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersInsert"; //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); //para el uso de dapper se recomienda el uso de parametros dinamicos
                                                          //parameters es una lista de parametros
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return result > 0;

            }
        }
        public async Task<bool> UpdateAsync(Customers customer)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "CustomersUpdate"; //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); //para el uso de dapper se recomienda el uso de parametros dinamicos
                                                          //parameters es una lista de parametros
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return result > 0;
            }
        }

        public async Task<IEnumerable<Customers>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {
                var query = "CustomersListWithPagination";
                var parameters = new DynamicParameters();

                parameters.Add("PageNumber", pageNumber);//el numero de la pagina a mostrar
                parameters.Add("PageSize", pageSize); //la cantidad de registros que se van a mostrar

                var result = await connection.QueryAsync<Customers>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int> CountAsync()
        {
            using var connection = _context.CreateConnection();
            var query = "select Count(*) from Customers"; //devuelve el valor total de registros que hay en la tabla
            var parameters = new DynamicParameters();

            var result = await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);// con el metodo ExecuteScalar ejecutamos la instruccion para saber cuantos registros tiene la tabla
            return result;
        }

        #endregion Fin Asíncronos

    }
}
