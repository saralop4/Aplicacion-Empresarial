using Ecommerce.Dominio.Entity;
using Ecommerce.Dominio.Interfaces;
using Ecommerce.Infraestructura.Interfaces;
using Ecommerce.Transversal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<Customers> GetSync(string customerId)
        {
            return await _customersRepository.GetSync(customerId);
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
