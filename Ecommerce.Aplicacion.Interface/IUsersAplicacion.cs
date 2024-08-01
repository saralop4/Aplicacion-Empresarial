using Ecommerce.Aplicacion.DTO;
using Ecommerce.Transversal.Common;

namespace Ecommerce.Aplicacion.Interface
{
    public interface IUsersAplicacion
    {
        Response<UsersDto>Authenticate(string username, string password);

    }
}
