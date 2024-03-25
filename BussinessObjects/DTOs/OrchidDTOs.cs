using BussinessObjects.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static BussinessObjects.DTOs.UpdateOrchidDTO;

namespace BussinessObjects.DTOs
{
    public class AddOrchidDTO
    {
        public class AddOrchidRequestJson
        {
            [Required(ErrorMessage = "Name is required")]
            [MaxLength(100, ErrorMessage = "Name can not be more than 100 characters")]
            public string Name { get; set; }

            [MinLength(1, ErrorMessage = "Description must be at least 1 characters")]
            [MaxLength(500, ErrorMessage = "Description can not be more than 500 characters")]
            public string? Description { get; set; }

            [MinLength(1, ErrorMessage = "Color must be at least 1 characters")]
            [MaxLength(50, ErrorMessage = "Color can not be more than 50 characters")]
            public string? Color { get; set; }

            [MinLength(1, ErrorMessage = "Origin must be at least 1 characters")]
            [MaxLength(50, ErrorMessage = "Origin can not be more than 50 characters")]
            public string? Origin { get; set; }

            [MinLength(1, ErrorMessage = "Species must be at least 1 characters")]
            [MaxLength(50, ErrorMessage = "Species can not be more than 50 characters")]
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
            [MaxLength(100, ErrorMessage = "Name can not be more than 100 characters")]
            public string? Name { get; set; }

            [MaxLength(500, ErrorMessage = "Description can not be more than 500 characters")]
            public string? Description { get; set; }

            [MaxLength(50, ErrorMessage = "Color can not be more than 50 characters")]
            public string? Color { get; set; }

            [MaxLength(50, ErrorMessage = "Origin can not be more than 50 characters")]
            public string? Origin { get; set; }

            [MaxLength(50, ErrorMessage = "Species can not be more than 50 characters")]
            public string? Species { get; set; }
        }

        public class UpdateOrchidRequestData
        {
            [Required(ErrorMessage = "OrchidId is required")]
            public Guid OrchidId { get; set; }
            public UpdateOrchidRequestJson? Json { get; set; }
            public IFormFile? ImageFile { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public DepositStatus? DepositStatus { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public ApprovalStatus? ApprovalStatus { get; set; }
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
            [Required(ErrorMessage = "OrchidId is required")]
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
            public IList<OrchidDTO>? Orchids { get; set; }
            public int Pages { get; set; }
        }

        public class GetOwnedOrchidListResponseData
        {
            public IList<OrchidDTO>? Orchids { get; set; }
            public int Pages { get; set; }
        }
        public class GetOwnedOrchidListResponse : AuthResponse<GetOwnedOrchidListResponseData>
        {
        }
    }
}
