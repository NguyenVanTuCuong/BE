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
        }

        public class AddOrchidRequestData
        {
            public AddOrchidRequestJson Json { get; set; }
            public IFormFile ImageFile { get; set; }
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

    public class OrchidDTO
    {
        public Guid OrchidId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public Guid OwnerId { get; set; }
        public DepositStatus DepositStatus { get; set; }
    }

    public class UpdateOrchidDTO
    {
        public class UpdateOrchidRequestJson
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
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
}
