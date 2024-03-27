using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessObjects.Models;

namespace Repositories.Transaction
{
    public interface ITransactionRepository : IGenericRepository<BussinessObjects.Models.Transaction>
    {
        Task<IList<BussinessObjects.Models.Transaction>> GetTransactionsListByUserId(Guid userId);
        Task<BussinessObjects.Models.Transaction> GetOneTransactionByUserId(Guid userId);
        Task<BussinessObjects.Models.Transaction> GetByIdIncludeOrchid(Guid transactionId);
    }
}
