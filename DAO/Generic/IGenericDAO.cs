using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Generic
{
    public interface IGenericDAO<T> where T : class
    {
        public Task<IList<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(Guid id);
        public Task<T> AddAsync(T entity);
        public Task<T> DeleteAsync(Guid id);
        public Task<T> UpdateAsync(Guid id, T entity);
    }
}
