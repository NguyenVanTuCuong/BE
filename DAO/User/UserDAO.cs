using DAO.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.User
{
    public class UserDAO : GenericDAO<BussinessObjects.Models.User>, IUserDAO
    {
        private static UserDAO instance = null;

        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDAO();
                }
                return instance;
            }
        }

        public async Task<BussinessObjects.Models.User?> GetByEmailAsync(string email) 
            => await _context.Users.FirstOrDefaultAsync(entity => entity.Email.Equals(email));
    }
}
