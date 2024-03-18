using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using BussinessObjects.Models;
using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.User
{
    public interface IOrchidRepository : IGenericRepository<Orchid>
    {
        public Task<IList<Orchid>> GetOrchidsPagination(int pageSize, int pageNumber);
        public Task<IList<Orchid>> SearchOrchids(string? name, string? description, DepositStatus? depositedStatus);
    }
}
