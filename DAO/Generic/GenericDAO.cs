using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Generic
{
    public class GenericDAO<T> : IGenericDAO<T> where T : class
    {
        private static GenericDAO<T> instance = null;
        protected readonly OrchidAuctionContext _context = null;

        public GenericDAO()
        {
            if (_context == null)
            {
                _context = new OrchidAuctionContext();
            }
        }

        public static GenericDAO<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GenericDAO<T>();
                }
                return instance;
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            var added = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<T> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity with ID {id} not found.");
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(Guid id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);

            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Entity with ID {id} not found.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();

            return existingEntity;
        }
    }
}
