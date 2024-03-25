using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using BussinessObjects.Models;
using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DepositRequest
{
    public interface IDepositRequestRepository : IGenericRepository<BussinessObjects.Models.DepositRequest>
    {
        public Task<List<BussinessObjects.Models.DepositRequest>> GetDepositRequestByUserIdPagination(Guid? userId);
        public Task<List<BussinessObjects.Models.DepositRequest>> GetAllIncludesAsync();
        public Task<BussinessObjects.Models.DepositRequest> GetByOrchidIdAndLatestCreatedDate(Guid orchidId);
        public Task<BussinessObjects.Models.DepositRequest> GetByOrchidId(Guid orchidId);
    }
}
