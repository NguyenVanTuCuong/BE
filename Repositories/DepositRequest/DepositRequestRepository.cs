using BussinessObjects.Models;
using DAO.DepositRequest;
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

        public async Task<List<BussinessObjects.Models.DepositRequest>> GetAllIncludesAsync()
            => await DepositRequestDAO.Instance.GetAllIncludesAsync();

        public async Task<List<BussinessObjects.Models.DepositRequest>> GetDepositRequestByUserIdPagination(Guid? userId)
            => await DepositRequestDAO.Instance.GetDepositRequestByUserIdPagination(userId);

        public async Task<BussinessObjects.Models.DepositRequest> GetByOrchidIdAndLatestCreatedDate(Guid orchidId)
            => await DepositRequestDAO.Instance.GetByOrchidIdAndLatestCreatedDate(orchidId);

        public async Task<BussinessObjects.Models.DepositRequest> GetByOrchidId(Guid orchidId)
            => await DepositRequestDAO.Instance.GetByOrchidId(orchidId);
    }
}
