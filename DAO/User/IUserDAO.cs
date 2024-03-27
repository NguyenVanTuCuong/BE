using DAO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.User
{
    public interface IUserDAO : IGenericDAO<BussinessObjects.Models.User>
    {
        Task<BussinessObjects.Models.User?> GetByEmailAsync(string email);
    }
}
