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
        public Task<Orchid?> GetOrchidByName(string name);
    }
}
