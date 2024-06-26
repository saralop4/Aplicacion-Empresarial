using Ecommerce.Dominio.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Dominio.Interfaces
{
    public interface IUsersDomain
    {
        Users Authenticate(string username, string password);
    }
}
