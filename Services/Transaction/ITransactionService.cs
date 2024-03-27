using BussinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BussinessObjects.DTOs.GetOrchidDTO;

namespace Services.Transaction
{
    public interface ITransactionService
    {
        Task<AddTransactionDTO.AddTransactionResponseData> AddTransaction(AddTransactionDTO.AddTransactionRequest request);
        Task<GetTransactionDTO.GetTransactionListResponseData> GetAllTransactionsPagination(int skip, int top);
        Task<GetTransactionDTO.GetTransactionListResponseData> GetOwnedTransactionsPagination(Guid ownerId, int skip, int top);
        Task<DeleteTransactionDTO.DeleteTransactionResponseData> DeleteTransaction(DeleteTransactionDTO.DeleteTransactionRequest request);
        Task<GetTransactionDTO.TransactionDTO> GetTransactionById(Guid transactionId);
    }
}
