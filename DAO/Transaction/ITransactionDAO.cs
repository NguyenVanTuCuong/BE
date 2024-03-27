using DAO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Transaction
{
    public interface ITransactionDAO : IGenericDAO<BussinessObjects.Models.Transaction>
    {
        Task<List<BussinessObjects.Models.Transaction>> GetListByUserId(Guid? userId);
        Task<BussinessObjects.Models.Transaction> GetOneByUserId(Guid? userId);
        Task<BussinessObjects.Models.Transaction> GetByIdIncludeOrchid(Guid? transactionId);
    }
}
