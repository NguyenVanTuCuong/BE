using BussinessObjects.Models;
using Repositories.Generic;
using Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Transactions
{
    public class TransactionRepository: GenericRepository<BussinessObjects.Models.Transaction>, ITransactionRepository
    {
        private readonly OrchidAuctionContext _context;

        public TransactionRepository()
        {
            _context = new OrchidAuctionContext();
        }
    }
}
