using Ecommerce.Dominio.Entity;

namespace Ecommerce.Dominio.Interfaces
{
    public interface IUsersDomain
    {
        Users Authenticate(string username, string password);
    }
}
