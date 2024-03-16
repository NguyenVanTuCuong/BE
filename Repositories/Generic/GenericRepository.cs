using BussinessObjects.Models;
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
        private readonly OrchidAuctionContext _context;

        public GenericRepository()
        {
            _context = new OrchidAuctionContext();
        }

        public async Task<T> AddAsync(T entity)
        {
            var added = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return added.Entity;
        }

        public Task<T> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task<T> UpdateAsync(Guid id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
