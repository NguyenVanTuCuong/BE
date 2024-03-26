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
        public Task<GetTransactionListResponse> GetOrchidsPagination(int skip, int top);
        public Task<GetOwnedTransactionListResponseData> GetOwnedOrchidsPagination(Guid ownerId, int skip, int top);
        public Task<UpdateTransactionDTO.UpdateTransactionResponseData> UpdateOrchid(UpdateTransactionDTO.UpdateTransactionRequest request);
        public Task<DeleteTransactionDTO.DeleteTrasactionResponseData> DeleteOrchid(DeleteTransactionDTO.DeleteTransactionRequest request);
        public Task<TransactionDTO> GetTrasactionById(Guid transactionId);
        public Task<GetTransactionListResponse> SearchTransaction(Guid? OrchidId, int skip, int top);
    }
}
