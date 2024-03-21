using BussinessObjects.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using static BussinessObjects.DTOs.UpdateOrchidDTO;

namespace BussinessObjects.DTOs
{
    public class AddOrchidDTO
    {
        public class AddOrchidRequestJson
        {
            public string Name { get; set; }
            public string? Description { get; set; }
            public string? Color { get; set; }
            public string? Origin { get; set; }
            public string? Species { get; set; }
        }

        public class AddOrchidRequestData
        {
            public AddOrchidRequestJson Json { get; set; }
            public IFormFile? ImageFile { get; set; }
        }

        public class AddOrchidRequest : AuthRequest<AddOrchidRequestData>
        {
        }

        public class AddOrchidResponseData
        {
            public Guid OrchidId { get; set; }
        }

        public class AddOrchidResponse : AuthResponse<AddOrchidResponseData>
        {
        }
    }

    public class UpdateOrchidDTO
    {
        public class UpdateOrchidRequestJson
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? Color { get; set; }
            public string? Origin { get; set; }
            public string? Species { get; set; }
        }

        public class UpdateOrchidRequestData
        {
            public Guid OrchidId { get; set; }
            public UpdateOrchidRequestJson? Json { get; set; }
            public IFormFile? ImageFile { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public DepositStatus? DepositStatus { get; set; }
        }

        public class UpdateOrchidRequest : AuthRequest<UpdateOrchidRequestData>
        {
        }

        public class UpdateOrchidResponseData
        {
            public Guid OrchidId { get; set; }
        }

        public class UpdateOrchidResponse : AuthResponse<UpdateOrchidResponseData>
        {
        }
    }

    public class DeleteOrchidDTO
    {
        public class DeleteOrchidRequest : AuthRequest<DeleteOrchidRequestData>
        {
        }
        public class DeleteOrchidRequestData
        {
            public Guid OrchidId { get; set; }
        }

        public class DeleteOrchidResponseData
        {
            public Guid OrchidId { get; set; }
        }
        public class DeleteOrchidResponse : AuthResponse<DeleteOrchidResponseData>
        {
        }
    }
    public class GetOrchidDTO
    {
        public class OrchidDTO
        {
            public Guid OrchidId { get; set; }
            public string Name { get; set; } = null!;
            public string? Description { get; set; }
            public string? ImageUrl { get; set; }
            public Guid OwnerId { get; set; }
            public string? Color { get; set; }
            public string? Origin { get; set; }
            public string? Species { get; set; }
            public ApprovalStatus ApprovalStatus { get; set; }
            public DepositStatus DepositedStatus { get; set; }
        }

        public class GetOrchidListResponse
        {
            public IList<OrchidDTO>? orchids { get; set; }
            public double pages { get; set; }
        }

        public class GetOwnedOrchidListResponseData
        {
            public IList<OrchidDTO>? orchids { get; set; }
            public double pages { get; set; }
        }
        public class GetOwnedOrchidListResponse : AuthResponse<GetOwnedOrchidListResponseData>
        {
        }
    }
}
