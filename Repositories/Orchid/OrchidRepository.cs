using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObjects.Models;
using BussinessObjects.DTOs;
using BussinessObjects.Enums;

namespace Repositories.User
{
    public class OrchidRepository : GenericRepository<Orchid>, IOrchidRepository
    {
        private readonly OrchidAuctionContext _context;

        public OrchidRepository()
        {
            _context = new OrchidAuctionContext();
        }

        public async Task<IList<Orchid>> GetOrchidsPagination(int pageSize, int pageNumber)
        {
            return await _context.Orchids
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IList<Orchid>> SearchOrchids(string? name, string? description, DepositStatus? depositedStatus)
        {
            IQueryable<Orchid> query = _context.Orchids;

            query = query.Where(o =>
                (string.IsNullOrWhiteSpace(name) || o.Name.Contains(name)) &&
                (string.IsNullOrWhiteSpace(description) || o.Description.Contains(description)) &&
                (!depositedStatus.HasValue || o.DepositedStatus == depositedStatus)
            );

            return await query.ToListAsync();
        }
    }
}
