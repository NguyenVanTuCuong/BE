using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.User
{
    public class OrchidRepository : GenericRepository<BussinessObjects.Models.Orchid>, IOrchidRepository
    {
        private readonly OrchidAuctionContext _context;

        public OrchidRepository()
        {
            _context = new OrchidAuctionContext();
        }
    }
}
