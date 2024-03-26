using BussinessObjects.Enums;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BussinessObjects.DTOs
{


    //ADD----------------------------------------------------------
    public class AddTransactionRequestJson
    {
        [Required(ErrorMessage = "TransactionId is required")]
        public Guid TransactionId { get; set; }
        [Required(ErrorMessage = "TransactionHash is required")]
        public string TransactionHash { get; set; } = null!;
        [Required(ErrorMessage = "Amount is required")]
        public float Amount { get; set; }
        [Required(ErrorMessage = "OrchidId is required")]
        public Guid OrchidId { get; set; }

        public class AddTransactionRequestData
        {
            public AddTransactionRequestJson Json { get; set; }
        }

        public class AddTransactionRequest : AuthRequest<AddTransactionRequestData>
        {
        }

        public class AddTransactionResponseData
        {
            public Guid OrchidId { get; set; }
        }

        public class AddTransactionResponse : AuthResponse<AddTransactionResponseData>
        {
        }
    }

    //UPDATE----------------------------------------------------------
    public class UpdateTransactionDTO
    {
        public class UpdateTransactionRequestJson
        {
            [Required(ErrorMessage = "TransactionId is required")]
            public Guid TransactionId { get; set; }
            [Required(ErrorMessage = "TransactionHash is required")]
            public string TransactionHash { get; set; } = null!;
            [Required(ErrorMessage = "Amount is required")]
            public float Amount { get; set; }
            [Required(ErrorMessage = "OrchidId is required")]
            public Guid OrchidId { get; set; }
        }

        public class UpdateTransactionRequestData
        {
            [Required(ErrorMessage = "TransactionId is required")]
            public Guid TransactionId { get; set; }
            public UpdateTransactionRequestJson? Json { get; set; }
        }

        public class UpdateTransactionRequest : AuthRequest<UpdateTransactionRequestData>
        {
        }

        public class UpdateTransactionResponseData
        {
            public Guid TransactionId { get; set; }
        }

        public class UpdateTransactionResponse : AuthResponse<UpdateTransactionResponseData>
        {
        }
    }

    //DELETE-----------------------------------------------------------------------------------
    public class DeleteTransactionDTO
    {
        public class DeleteTransactionRequest : AuthRequest<DeleteTransactionRequestData>
        {
        }
        public class DeleteTransactionRequestData
        {
            [Required(ErrorMessage = "TransactionId is required")]
            public Guid TransactionId { get; set; }
        }

        public class DeleteTrasactionResponseData
        {
            public Guid OrchidId { get; set; }
        }

        public class DeleteOrchidResponse : AuthResponse<DeleteTrasactionResponseData>
        {
        }
    }

    //GET-----------------------------------------------------------------------------------
    public class GetTransactionDTO
    {
        public class TransactionDTO
        {
            public Guid TransactionId { get; set; }
            public string TransactionHash { get; set; } = null!;
            public float Amount { get; set; }
            public Guid OrchidId { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public class GetTransactionListResponse
        {
            public IList<TransactionDTO>? Orchids { get; set; }
            public int Pages { get; set; }
        }

        public class GetOwnedTransactionListResponseData
        {
            public IList<TransactionDTO>? Orchids { get; set; }
            public int Pages { get; set; }
        }

        public class GetOwnedTransactionListResponse : AuthResponse<GetOwnedTransactionListResponseData>
        {
        }
    }

}
