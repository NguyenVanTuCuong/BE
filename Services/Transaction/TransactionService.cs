using AutoMapper;
using BussinessObjects.DTOs;
using Repositories.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BussinessObjects.DTOs.GetOrchidDTO;
using static BussinessObjects.DTOs.GetTransactionDTO;

namespace Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public class GetTransactionException : Exception
        {
            public enum StatusCodeEnum
            {
                TransactionNotFound
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }
            public GetTransactionException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }
        public class DeleteTransactionException : Exception
        {
            public enum StatusCodeEnum
            {
                DeleteTransactionFailed,
                TransactionNotFound,
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }
            public DeleteTransactionException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public async Task<AddTransactionDTO.AddTransactionResponseData> AddTransaction(AddTransactionDTO.AddTransactionRequest request)
        {
            var transaction = new BussinessObjects.Models.Transaction
            {
                TransactionHash = request.Data.TransactionHash,
                Amount = request.Data.Amount,
                OrchidId = request.Data.OrchidId,
            };

            var created = await _transactionRepository.AddAsync(transaction);

            return new AddTransactionDTO.AddTransactionResponseData
            {
                TransactionId = created.TransactionId,
            };
        }

        public async Task<DeleteTransactionDTO.DeleteTransactionResponseData> DeleteTransaction(DeleteTransactionDTO.DeleteTransactionRequest request)
        {
            var existing = await _transactionRepository.GetByIdIncludeOrchid(request.Data.TransactionId);
            if (existing == null)
            {
                throw new DeleteTransactionException(
                    DeleteTransactionException.StatusCodeEnum.TransactionNotFound, 
                    "Transaction not found");
            }

            await _transactionRepository.DeleteAsync(request.Data.TransactionId);

            return new DeleteTransactionDTO.DeleteTransactionResponseData()
            {
                TransactionId = existing.TransactionId,
            };

        }

        public async Task<GetTransactionListResponseData> GetOwnedTransactionsPagination(Guid ownerId, int skip, int top)
        {
            var queryable = await _transactionRepository.GetTransactionsListByUserId(ownerId);
            var pagination = queryable.Skip(skip).Take(top).AsQueryable();
            var totalCount = queryable.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<TransactionDTO>>(pagination);

            return new GetTransactionListResponseData
            {
                Transactions = response,
                Pages = (int)maxPage
            };
        }

        public async Task<GetTransactionListResponseData> GetAllTransactionsPagination(int skip, int top)
        {
            var queryable = await _transactionRepository.GetAllAsync();
            var pagination = queryable.Skip(skip).Take(top).AsQueryable();
            var totalCount = queryable.Count();
            var maxPage = totalCount >= top ? Math.Ceiling((double)totalCount / top) : 1;
            var response = _mapper.Map<IList<TransactionDTO>>(pagination);

            return new GetTransactionListResponseData
            {
                Transactions = response,
                Pages = (int)maxPage
            };
        }

        public async Task<TransactionDTO> GetTransactionById(Guid transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null)
            {
                throw new GetTransactionException(GetTransactionException.StatusCodeEnum.TransactionNotFound, "Transaction not found");
            }
            return _mapper.Map<TransactionDTO>(transaction);
        }
    }
}
