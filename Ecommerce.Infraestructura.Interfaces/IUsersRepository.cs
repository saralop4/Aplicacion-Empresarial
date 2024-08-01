using Ecommerce.Dominio.Entity;

namespace Ecommerce.Infraestructura.Interfaces
{
    public interface IUsersRepository : IGenericRepository<Users>
    {

        Users Authenticate(string userName, string password);
    }
}
