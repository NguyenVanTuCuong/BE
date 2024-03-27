using BussinessObjects.Models;
using DAO.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public async Task<IList<T>> GetAllAsync() => await GenericDAO<T>.Instance.GetAllAsync();

        public async Task<T?> GetByIdAsync(Guid id) => await GenericDAO<T>.Instance.GetByIdAsync(id);

        public async Task<T> AddAsync(T entity) => await GenericDAO<T>.Instance.AddAsync(entity);

        public async Task<T> DeleteAsync(Guid id) => await GenericDAO<T>.Instance.DeleteAsync(id);

        public async Task<T> UpdateAsync(Guid id, T entity) => await GenericDAO<T>.Instance.UpdateAsync(id, entity);
    }
}
