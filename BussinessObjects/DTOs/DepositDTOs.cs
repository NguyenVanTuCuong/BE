using BussinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BussinessObjects.DTOs.AddOrchidDTO;
using static BussinessObjects.DTOs.GetOrchidDTO;

namespace BussinessObjects.DTOs
{
    public class AddDepositRequestDTO
    {
        public class AddDepositRequestData
        {
            public Guid OrchidId { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string WalletAddress { get; set; }
        }

        public class AddDepositRequest : AuthRequest<AddDepositRequestData>
        {
        }

        public class AddDepositResponseData
        {
            public Guid DepositRequestId { get; set; }
        }

        public class AddDepositResponse : AuthResponse<AddDepositResponseData>
        {
        }
    }

    public class UpdateDepositRequestDTO
    {
        public class UpdateDepositRequestData
        {
            public Guid DepositRequestId { get; set; }
            public Guid? OrchidId { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? WalletAddress { get; set; }
            public RequestStatus? requestStatus { get; set; }
        }

        public class UpdateDepositRequest : AuthRequest<UpdateDepositRequestData>
        {

        }

        public class UpdateDepositResponseData
        {
            public Guid DepositRequestId { get; set; }
        }

        public class UpdateDepositResponse : AuthResponse<UpdateDepositResponseData>
        {
        }
    }

    public class GetDepositDTO
    {
        public class DepositDTO
        {
            public Guid DepositRequestId { get; set; }
            public Guid OrchidId { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string WalletAddress { get; set; }
            public DateTime CreatedAt {  get; set; }
            public DateTime UpdatedAt { get; set; }

            public RequestStatus? RequestStatus { get; set; }
            public OrchidDTO Orchid { get; set; }
        }

        public class GetDepositListResponse
        {
            public IList<DepositDTO>? deposits { get; set; }
            public double pages { get; set; }
        }

        public class GetDepositResponse : AuthResponse<GetDepositListResponse>
        {
        }
    }

    public class ReviewDepositRequestDTO
    {
        public class ReviewDepositRequestData
        {
            public Guid DepositRequestId { get; set; }
            public RequestStatus RequestStatus {  get; set; }
        }

        public class ReviewDepositRequest : AuthRequest<ReviewDepositRequestData>
        {

        }

        public class ReviewDepositResponseData
        {
        }

        public class ReviewDepositResponse : AuthResponse<ReviewDepositResponseData>
        {
        }
    }

}
