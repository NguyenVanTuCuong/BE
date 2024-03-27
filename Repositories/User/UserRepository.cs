using BussinessObjects.Models;
using DAO.User;
using Microsoft.EntityFrameworkCore;
using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.User
{
    public class UserRepository : GenericRepository<BussinessObjects.Models.User>, IUserRepository
    {
        public async Task<BussinessObjects.Models.User?> GetByEmailAsync(string email) 
            => await UserDAO.Instance.GetByEmailAsync(email);
    }
}
