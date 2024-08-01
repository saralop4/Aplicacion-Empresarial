using System;

namespace Ecommerce.Infraestructura.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //son propiedades unicamente de lecturas y no se deben modificar.
        ICustomersRepository CustomersRepository { get; }
        IUsersRepository UsersRepository { get; }

    }
}
