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
    public class UserRepository : GenericRepository<BussinessObjects.Models.User>, IUserRepository
    {
        private readonly OrchidAuctionContext _context;

        public UserRepository()
        {
            _context = new OrchidAuctionContext();
        }

        public async Task<BussinessObjects.Models.User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(entity => entity.Email.Equals(email));
        }

    }
}
