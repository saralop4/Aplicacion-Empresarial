using Ecommerce.Dominio.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructura.Interfaces
{

    //implementacion del patron repositorio generico
    public interface ICustomersRepository : IGenericRepository<Customers>
    {
      //por defecto está heredando los metodos de la interfaz generica, lo que se puede es agregar metodos que no tenga
      //la interfaz generica
    }
}
