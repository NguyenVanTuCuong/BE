using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObjects.Models;

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

        public Task<Orchid?> GetOrchidByName(string name)
        {
            return _context.Orchids
                .Where(o => o.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}
