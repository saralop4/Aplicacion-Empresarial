﻿using Dapper;
using Ecommerce.Dominio.Entity;
using Ecommerce.Infraestructura.Data;
using Ecommerce.Infraestructura.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructura.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _context;
        public UsersRepository(DapperContext context)
        {
            _context = context;
        }
        public Users Authenticate(string userName, string password)
        {
            using(var connection = _context.CreateConnection())
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

        public int Count()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(string customerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Users Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Users> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Users>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Users> GetAllWithPagination(int pageNumber, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Users>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public Task<Users> GetAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Users entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> InsertAsync(Users entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Users entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(Users entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
