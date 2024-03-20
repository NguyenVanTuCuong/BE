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
        public class DepositForNftRequestData
        {
            public string Address { get; set;  }
            public Guid OrchidId { get; set; }
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

    public class WithdawNftDTO
    {
        public class WithdawNftRequestData
        {
            public int TokenId { get; set; }
        }

        public class WithdawNftRequest : AuthRequest<WithdawNftRequestData>
        {
        }

        public class WithdawNftResponseData
        {
            public string TransactionHash { get; set; }
        }
        public class WithdawNftResponse : AuthResponse<WithdawNftResponseData>
        {
        }


    }
}