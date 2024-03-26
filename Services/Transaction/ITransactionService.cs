using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BussinessObjects.DTOs.GetTransactionDTO;

namespace Services.Transaction
{
    public interface ITransactionService
    {
        //ADD
        public Task<AddTransactionRequestJson.AddTransactionResponseData>
                AddTransactionData(AddTransactionRequestJson.AddTransactionRequest request);
        // GET LIST
        public Task<GetTransactionListResponse> GetTransactionPagination(int skip, int top);
        public Task<GetOwnedTransactionListResponseData> GetOwnedTransactionPagination(Guid ownerId, int skip, int top);
        public Task<UpdateTransactionDTO.UpdateTransactionResponseData> UpdateTransaction(UpdateTransactionDTO.UpdateTransactionRequest request);
        public Task<DeleteTransactionDTO.DeleteTrasactionResponseData> DeleteTransaction(DeleteTransactionDTO.DeleteTransactionRequest request);
        public Task<TransactionDTO> GetTrasactionById(Guid transactionId);
        public Task<GetTransactionListResponse> SearchTransaction(Guid? OrchidId, int skip, int top);
    }
}
