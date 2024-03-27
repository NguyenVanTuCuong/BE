using System.ComponentModel.DataAnnotations;


namespace BussinessObjects.DTOs
{
    public class AddTransactionDTO
    {
        public class AddTransactionRequestData
        {
            [Required(ErrorMessage = "TransactionHash is required")]
            public string TransactionHash { get; set; }

            [Required(ErrorMessage = "Amount is required")]
            public float Amount { get; set; }

            [Required(ErrorMessage = "OrchidId is required")]
            public Guid OrchidId { get; set; }

        }

        public class AddTransactionRequest : AuthRequest<AddTransactionRequestData>
        {
        }

        public class AddTransactionResponseData
        {
            public Guid TransactionId { get; set; }
        }

        public class AddTransactionResponse : AuthResponse<AddTransactionResponseData>
        {
        }
    }

    public class DeleteTransactionDTO
    {
        public class DeleteTransactionRequestData
        {
            [Required(ErrorMessage = "TransactionId is required")]
            public Guid TransactionId { get; set; }
        }

        public class DeleteTransactionRequest : AuthRequest<DeleteTransactionRequestData>
        {
        }

        public class DeleteTransactionResponseData
        {
            public Guid TransactionId { get; set; }
        }

        public class DeleteTransactionResponse : AuthResponse<DeleteTransactionResponseData>
        {
        }
    }

    public class GetTransactionDTO
    {
        public class TransactionDTO
        {
            public Guid TransactionId { get; set; }
            public string TransactionHash { get; set; }
            public float Amount { get; set; }
            public Guid OrchidId { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public class GetTransactionListResponseData
        {
            public IList<TransactionDTO> Transactions { get; set; }
            public int Pages { get; set; }
        }

        public class GetTransactionListResponse : AuthResponse<GetTransactionListResponseData>
        {
        }      
    }
}
