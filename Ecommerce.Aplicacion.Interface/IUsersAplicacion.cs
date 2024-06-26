using Ecommerce.Aplicacion.DTO;
using Ecommerce.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Aplicacion.Interface
{
    public interface IUsersAplicacion
    {
        Response<UsersDto>Authenticate(string username, string password);

    }
}
