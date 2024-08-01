using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructura.Interfaces
{
    public interface IGenericRepository<T> where T: class //agregamos una restriccion para que T siempre sea de tipo class
    {
        #region Metodos Sincronos
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string customerId);
        T Get(string id);
        IEnumerable<T> GetAll();

        IEnumerable<T> GetAllWithPagination(int pageNumber, int pageSize);  
        int Count();

        #endregion

        #region Metodos Asincronos
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();

        #endregion

    }
}
