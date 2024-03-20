using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObjects.DTOs;

namespace Services.Blockchain
{
    public interface IBlockchainService
    {
        public Task<DepositForNftDTO.DepositForNftResponseData> DepositForNft(DepositForNftDTO.DepositForNftRequest request);

        public Task<WithdawNftDTO.WithdawNftResponseData> WithdrawNft(WithdawNftDTO.WithdawNftRequest request);
    }
}
