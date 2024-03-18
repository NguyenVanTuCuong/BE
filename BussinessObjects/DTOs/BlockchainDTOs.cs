using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BussinessObjects.DTOs
{
    public class DepositForNftDTO
    {
        public class DepositForNftRequestJson
        {
            public string Address { get; set;  }
            public Guid OrchidId { get; set; }
        }

        public class DepositForNftRequestData
        {
            public DepositForNftRequestJson Json { get; set; }
            public IFormFile ImageFile { get; set; }
        }


        public class DepositForNftRequest : AuthRequest<DepositForNftRequestData>
        {
        }

        public class DepositForNftResponseData
        {
            public string TransactionHash { get; set; }
        }
        public class DepositForNftResponse : AuthResponse<DepositForNftResponseData>
        {
        }
    }
}