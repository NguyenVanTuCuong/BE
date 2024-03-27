using DAO.DepositRequest;
using DAO.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Transaction
{
    public class TransactionDAO : GenericDAO<BussinessObjects.Models.Transaction>, ITransactionDAO
    {
        private static TransactionDAO instance = null;

        public static TransactionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransactionDAO();
                }
                return instance;
            }
        }

        public async Task<List<BussinessObjects.Models.Transaction>> GetListByUserId(Guid? userId)
            => await _context.Transactions.Where(entity => entity.Orchid.OwnerId == userId).ToListAsync();

        public async Task<BussinessObjects.Models.Transaction> GetOneByUserId(Guid? userId)
            => await _context.Transactions.Where(entity => entity.Orchid.OwnerId == userId).FirstOrDefaultAsync();

        public async Task<BussinessObjects.Models.Transaction> GetByIdIncludeOrchid(Guid? transactionId)
            => await _context.Transactions.Include(entity => entity.Orchid).Where(entity => entity.TransactionId == transactionId).FirstOrDefaultAsync();
    }
}
