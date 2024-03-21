using BussinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BussinessObjects.DTOs.AddOrchidDTO;
using static BussinessObjects.DTOs.GetOrchidDTO;

namespace BussinessObjects.DTOs
{
    public class AddDepositRequestDTO
    {
        public class AddDepositRequestRequestData
        {
            [Required(ErrorMessage = "OrchidId is required")]
            public Guid OrchidId { get; set; }

            [MinLength(1, ErrorMessage = "Title must be at least 1 characters")]
            [MaxLength(255, ErrorMessage = "Title can not be more than 255 characters")]
            public string? Title { get; set; }

            [MinLength(1, ErrorMessage = "Description must be at least 1 characters")]
            [MaxLength(255, ErrorMessage = "Description can not be more than 255 characters")]
            public string? Description { get; set; }

            [Required(ErrorMessage = "WalletAddress is required")]
            [MaxLength(100, ErrorMessage = "WalletAddress can not be more than 100 characters")]
            public string WalletAddress { get; set; }
        }

        public class AddDepositRequestRequest : AuthRequest<AddDepositRequestRequestData>
        {
        }

        public class AddDepositRequestResponseData
        {
            public Guid DepositRequestId { get; set; }
        }

        public class AddDepositRequestResponse : AuthResponse<AddDepositRequestResponseData>
        {
        }
    }

    public class UpdateDepositRequestDTO
    {
        public class UpdateDepositRequestRequestData
        {
            [Required(ErrorMessage = "DepositRequestId is required")]
            public Guid DepositRequestId { get; set; }

            [Required(ErrorMessage = "OrchidId is required")]
            public Guid OrchidId { get; set; }

            [MinLength(1, ErrorMessage = "Title must be at least 1 characters")]
            [MaxLength(255, ErrorMessage = "Title can not be more than 255 characters")]
            public string? Title { get; set; }

            [MinLength(1, ErrorMessage = "Description must be at least 1 characters")]
            [MaxLength(255, ErrorMessage = "Description can not be more than 255 characters")]
            public string? Description { get; set; }

            [Required(ErrorMessage = "WalletAddress is required")]
            [MaxLength(100, ErrorMessage = "WalletAddress can not be more than 100 characters")]
            public string WalletAddress { get; set; }

            [Required(ErrorMessage = "RequestStatus is required")]
            public RequestStatus? requestStatus { get; set; }
        }

        public class UpdateDepositRequestRequest : AuthRequest<UpdateDepositRequestRequestData>
        {

        }

        public class UpdateDepositRequestResponseData
        {
            public Guid DepositRequestId { get; set; }
        }

        public class UpdateDepositRequestResponse : AuthResponse<UpdateDepositRequestResponseData>
        {
        }
    }

    public class GetDepositRequestDTO
    {
        public class DepositRequestDTO
        {
            public Guid DepositRequestId { get; set; }
            public Guid OrchidId { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string WalletAddress { get; set; }
            public RequestStatus? RequestStatus { get; set; }
        }

        public class GetDepositRequestListResponse
        {
            public IList<DepositRequestDTO>? Deposits { get; set; }
            public int Pages { get; set; }
        }

        public class GetDepositResponse : AuthResponse<GetDepositRequestListResponse>
        {
        }
    }
}
