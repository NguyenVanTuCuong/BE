using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Generic;
using Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DepositRequest
{
    public class DepositRequestRepository : GenericRepository<BussinessObjects.Models.DepositRequest>, IDepositRequestRepository
    {
        private readonly OrchidAuctionContext _context;

        public DepositRequestRepository()
        {
            _context = new OrchidAuctionContext();
        }
        public async Task<List<BussinessObjects.Models.DepositRequest>> GetDepositRequestByUserIdPagination(Guid? userId)
        {
            var depositRequests = await _context.DepositRequests
            .Include(dr => dr.Orchid)
            .Where(dr => dr.Orchid.OwnerId == userId)
            .ToListAsync();

            return depositRequests;
        }

        public async Task<BussinessObjects.Models.DepositRequest> GetByOrchidIdAndLatestCreatedDate(Guid orchidId)
        {
            //get deposit request by orchid id and latest created date
            var depositRequest = await _context.DepositRequests
                .Include(dr => dr.Orchid)
                .Where(dr => dr.OrchidId == orchidId)
                .OrderByDescending(dr => dr.CreatedAt)
                .FirstOrDefaultAsync();

            return depositRequest;
        }
    }
}
