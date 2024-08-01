using Ecommerce.Dominio.Entity;
using Ecommerce.Dominio.Interfaces;
using Ecommerce.Infraestructura.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Core
{
    public class CustomersDomain : ICustomersDomain
    {
        //interfaz de Infraestructura
        private readonly ICustomersRepository _customersRepository;
        public CustomersDomain(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;

        }

        //La clase CustomersDomain no necesita saber cómo se implementa el método Delete.Solo necesita saber que el método existe en la interfaz ICustomersRepository.

        //Cuando se llama al método Delete de CustomersDomain, este a su vez llama al método Delete de _customersRepository, que es una instancia de ICustomersRepository. 
        //La implementación exacta de Delete que se llama depende de la clase que se pasó al constructor de CustomersDomain cuando se creó la instancia.

        //Por ejemplo, si pasaste una instancia de CustomersRepository al constructor de CustomersDomain, entonces el método Delete de CustomersRepository será el que se llame.
        //Si tuvieras otra clase que implementara ICustomersRepository y pasaras una instancia de esa clase al constructor, entonces se llamaría al método Delete de esa clase.
        //Esto es posible gracias al  polimorfismo, un principio fundamental de la programación orientada a objetos.El polimorfismo permite que un objeto se comporte de diferentes 
        //maneras dependiendo de su tipo en tiempo de ejecución.En este caso, _customersRepository puede ser cualquier objeto que implemente ICustomersRepository, y el método Delete
        //que se llama es el que corresponde a la implementación real del objeto.
        public bool Delete(string customerId)
        {
            return _customersRepository.Delete(customerId);
        }
        public async Task<bool> DeleteAsync(string customerId)
        {
            return await _customersRepository.DeleteAsync(customerId);
        }

        public Customers Get(string customerId)
        {
            return _customersRepository.Get(customerId);
        }

        public IEnumerable<Customers> GetAll()
        {
            return _customersRepository.GetAll();
        }

        public Task<IEnumerable<Customers>> GetAllAsync()
        {
            return _customersRepository.GetAllAsync();
        }

        public async Task<Customers> GetAsync(string customerId)
        {
            return await _customersRepository.GetAsync(customerId);
        }

        public bool Insert(Customers customer)
        {
           return _customersRepository.Insert(customer);    
        }

        public async Task<bool> InsertAsync(Customers customer)
        {
            return  await _customersRepository.InsertAsync(customer);
        }

        public bool Update(Customers customer)
        {
            return _customersRepository.Update(customer);
        }

        public async Task<bool> UpdateAsync(Customers customer)
        {
            return await _customersRepository.UpdateAsync(customer);
        }
    }
}
