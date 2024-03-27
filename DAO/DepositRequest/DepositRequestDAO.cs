using DAO.Generic;
using DAO.Orchid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DepositRequest
{
    public class DepositRequestDAO : GenericDAO<BussinessObjects.Models.DepositRequest>, IDepositRequestDAO
    {
        private static DepositRequestDAO instance = null;

        public static DepositRequestDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DepositRequestDAO();
                }
                return instance;
            }
        }

        public async Task<List<BussinessObjects.Models.DepositRequest>> GetDepositRequestByUserIdPagination(Guid? userId) 
            => await _context.DepositRequests.Where(entity => entity.Orchid.OwnerId == userId).ToListAsync();

        public async Task<List<BussinessObjects.Models.DepositRequest>> GetAllIncludesAsync() 
            => await _context.DepositRequests.Include(entity => entity.Orchid).ToListAsync();

        public async Task<BussinessObjects.Models.DepositRequest> GetByOrchidIdAndLatestCreatedDate(Guid orchidId) 
            => await _context.DepositRequests.Where(entity => entity.OrchidId == orchidId).OrderByDescending(entity => entity.CreatedAt).FirstOrDefaultAsync();

        public async Task<BussinessObjects.Models.DepositRequest> GetByOrchidId(Guid orchidId) 
            => await _context.DepositRequests.FirstOrDefaultAsync(entity => entity.OrchidId == orchidId);
    }
}
