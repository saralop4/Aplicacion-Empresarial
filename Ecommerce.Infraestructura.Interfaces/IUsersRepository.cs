using Ecommerce.Dominio.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infraestructura.Interfaces
{
    public interface IUsersRepository : IGenericRepository<Users>
    {

        Users Authenticate(string userName, string password);
    }
}
