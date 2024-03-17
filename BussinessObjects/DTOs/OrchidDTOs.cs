using Microsoft.AspNetCore.Http;

namespace BussinessObjects.DTOs
{
    public class AddOrchidDTO
    {
        public class AddOrchidRequestJson
        {
            public string Name { get; set; }
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
}
