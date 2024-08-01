using Ecommerce.Dominio.Entity;
using Ecommerce.Dominio.Interfaces;
using Ecommerce.Infraestructura.Interfaces;

namespace Ecommerce.Dominio.Core
{
    public class UsersDomain : IUsersDomain
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public Users Authenticate(string username, string password)
        {
            return _unitOfWork.UsersRepository.Authenticate(username, password);
        }
    }
}
