using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.User
{
    public interface IUserRepository : IGenericRepository<BussinessObjects.Models.User>
    {
        public Task<BussinessObjects.Models.User?> GetByEmailAsync(string email);
    }
}
