using BussinessObjects.Models;
using DAO.Transaction;
using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Transaction
{
    public class TransactionRepository : GenericRepository<BussinessObjects.Models.Transaction>, ITransactionRepository
    {
        public async Task<IList<BussinessObjects.Models.Transaction>> GetTransactionsListByUserId(Guid userId)
            => await TransactionDAO.Instance.GetListByUserId(userId);

        public async Task<BussinessObjects.Models.Transaction> GetOneTransactionByUserId(Guid userId)
            => await TransactionDAO.Instance.GetOneByUserId(userId);

        public async Task<BussinessObjects.Models.Transaction> GetByIdIncludeOrchid(Guid transactionId)
            => await TransactionDAO.Instance.GetByIdIncludeOrchid(transactionId);
    }
}
