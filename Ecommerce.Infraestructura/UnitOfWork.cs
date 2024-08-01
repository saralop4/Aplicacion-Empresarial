using Ecommerce.Infraestructura.Interfaces;

namespace Ecommerce.Infraestructura.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomersRepository CustomersRepository { get; }

        public IUsersRepository UsersRepository { get;  }

        public UnitOfWork(ICustomersRepository customersRepository, IUsersRepository usersRepository)
        {
            CustomersRepository = customersRepository;
            UsersRepository = usersRepository;
        }   

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);   //this quiere decir el objeto actual
        }
    }
}
