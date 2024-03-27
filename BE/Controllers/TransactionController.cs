using BussinessObjects.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.Orchid;
using Services.Transaction;

namespace BE.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IJwtService _jwtService;

        public TransactionController(ITransactionService transactionService, IJwtService jwtService)
        {
            _transactionService = transactionService;
            _jwtService = jwtService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] AddTransactionDTO.AddTransactionRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                var transaction = await _transactionService.AddTransaction(new AddTransactionDTO.AddTransactionRequest()
                {
                    UserId = userId.Value,
                    Data = data
                });

                return Created("null", new AddTransactionDTO.AddTransactionResponse
                {
                    Data = transaction,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }

                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllTransactionsPagination(int skip, int top)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                var transactions = await _transactionService.GetAllTransactionsPagination(skip, top);

                return Ok(new GetTransactionDTO.GetTransactionListResponse
                {
                    Data = transactions,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [HttpGet("owned")]
        public async Task<IActionResult> GetOwnedTransactionsPagination(int skip, int top)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                var transactions = await _transactionService.GetOwnedTransactionsPagination(userId.Value, skip, top);

                return Ok(new GetTransactionDTO.GetTransactionListResponse
                {
                    Data = transactions,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteTransaction(Guid transactionId)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                var transaction = await _transactionService.DeleteTransaction(new DeleteTransactionDTO.DeleteTransactionRequest()
                {
                    UserId = userId.Value,
                    Data = new DeleteTransactionDTO.DeleteTransactionRequestData
                    {
                        TransactionId = transactionId
                    }
                });

                return Ok(new DeleteTransactionDTO.DeleteTransactionResponse
                {
                    Data = transaction,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (TransactionService.DeleteTransactionException e)
            {
                switch (e.StatusCode)
                {
                    case TransactionService.DeleteTransactionException.StatusCodeEnum.TransactionNotFound:
                        return NotFound(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }
    }
}
